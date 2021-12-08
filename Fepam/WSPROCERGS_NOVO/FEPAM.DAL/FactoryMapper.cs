/****************************************************************************/
/* FEPAM - Fundação Estadual de Proteção Ambiental                          */
/* Projeto: Mapper para classe de factories                                 */
/* Author: Leonardo Tremper                                                 */
/*                                                                          */
/*                                                                          */
/* Date Generated: 07/11/2007                                               */
/*                                                                          */
/* Microsoft.Practices                                                      */
/****************************************************************************/
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Reflection;


namespace FEPAM.DAL
{
    public class FactoryMapper<T,T2,T3>
    {
        public static T3 Mapping(T obj1, T2 obj2, T3 ObjOut)
        {
            ArrayList _passedBy = new ArrayList();
            Hashtable PropertiesOfObj1 = new Hashtable();
            Type t1 = obj1.GetType();
            PropertyInfo[] pis1 = t1.GetProperties();
            for (int i = 0; i < pis1.Length; i++)
            {
                PropertyInfo pi = (PropertyInfo)pis1.GetValue(i);
                PropertiesOfObj1.Add(pi.Name, pi.GetValue(obj1, new object[] { }));
            }
            Hashtable PropertiesOfObj2 = new Hashtable();
            Type t2 = obj2.GetType();
            PropertyInfo[] pis2 = t2.GetProperties();
            for (int i = 0; i < pis2.Length; i++)
            {
                PropertyInfo pi = (PropertyInfo)pis2.GetValue(i);
                PropertiesOfObj2.Add(pi.Name, pi.GetValue(obj2, new object[] { }));
            }

            IDictionaryEnumerator Enumerator1 = PropertiesOfObj1.GetEnumerator();
            while (Enumerator1.MoveNext())
            {
                if (ObjOut.GetType().GetProperty(Enumerator1.Key.ToString()) != null)
                {
                    ObjOut.GetType().GetProperty(Enumerator1.Key.ToString()).SetValue(ObjOut, Enumerator1.Value, null);
                    _passedBy.Add(Enumerator1.Key.ToString());
                }
            }
            IDictionaryEnumerator Enumerator2 = PropertiesOfObj2.GetEnumerator();
            while (Enumerator2.MoveNext())
            {
                if (ObjOut.GetType().GetProperty(Enumerator2.Key.ToString()) != null)
                {
                    ObjOut.GetType().GetProperty(Enumerator2.Key.ToString()).SetValue(ObjOut, Enumerator2.Value, null);
                }
            }

            return ObjOut;
        }

        public static T MapTo(T2 obj, T objOut)
        {
            Hashtable PropertiesOfMyObject = new Hashtable();
            Type t = obj.GetType();
            PropertyInfo[] pis = t.GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                PropertyInfo pi = (PropertyInfo)pis.GetValue(i);
                PropertiesOfMyObject.Add(pi.Name, pi.GetValue(obj, new object[] { }));
            }
            IDictionaryEnumerator Enumerator = PropertiesOfMyObject.GetEnumerator();
            while (Enumerator.MoveNext())
            {
                if (objOut.GetType().GetProperty(Enumerator.Key.ToString()) != null)
                {
                    objOut.GetType().GetProperty(Enumerator.Key.ToString()).SetValue(objOut, Enumerator.Value, null);
                }
            }
            return objOut;
        }
    }
}
