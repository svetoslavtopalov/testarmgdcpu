using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace demomvp
{
    internal class ConnectionUtilities
    {
        public static IEnumerable<ServicePoint> ListServicePoints(StringBuilder builder)
        {
            FieldInfo field = typeof(ServicePointManager).GetField("s_ServicePointTable", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            Hashtable value = (Hashtable)field.GetValue(null);
            List<object> list = value.Keys.Cast<object>().ToList<object>();
            builder.AppendLine(string.Format("<BR/><BR/>ServicePoint count: {0}, DefaultConnectionLimit: {1}", list.Count, ServicePointManager.DefaultConnectionLimit));
            foreach (object obj in list)
            {
                WeakReference item = (WeakReference)value[obj];
                if (item == null)
                {
                    continue;
                }
                object target = item.Target;
                if (target == null)
                {
                    continue;
                }
                yield return target as ServicePoint;
            }
        }

        public static string PrintConnections()
        {
            var builder = new StringBuilder();
            foreach (ServicePoint servicePoint in ListServicePoints(builder))
            {
                builder.Append(PrintServicePointConnections(servicePoint));
            }

            return builder.ToString();
        }

        public static string PrintServicePointConnections(ServicePoint sp)
        {
            var builder = new StringBuilder();
            Type type = sp.GetType();
            BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo field = type.GetField("m_ConnectionGroupList", bindingFlag);
            Hashtable value = (Hashtable)field.GetValue(sp);
            List<object> list = value.Keys.Cast<object>().ToList<object>();
            int count = 0;
            builder.AppendLine(string.Format("<BR/><BR/>ServicePoint: {0} (Connection Limit: {1}, Reported connections: {2})", sp.Address, sp.ConnectionLimit, sp.CurrentConnections));
            foreach (object obj in list)
            {
                object item = value[obj];
                FieldInfo fieldInfo = item.GetType().GetField("m_ConnectionList", bindingFlag);
                ArrayList arrayLists = (ArrayList)fieldInfo.GetValue(item);
                builder.AppendLine(string.Format("{0}", obj));
                count += arrayLists.Count;
            }
            builder.AppendLine(string.Format("<BR/><BR/>ConnectionGroupCount: {0}, Total Connections: {1}", list.Count, count));

            return builder.ToString();
        }
    }
}