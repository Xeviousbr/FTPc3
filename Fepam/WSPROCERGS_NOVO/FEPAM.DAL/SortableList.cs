using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Data;

namespace FEPAM.DAL
{
    public class SortableList<T> : List<T>
    {
        private string _propertyName;
        private bool _ascending;

        public void Sort(string propertyName)
        {
            if (propertyName != null)
            {
                char[] splitter = { ' ' };
                string[] temp = propertyName.Split(splitter);
                bool asc = true;
                if ((temp.Length > 1) && (temp[1] == "DESC"))
                {
                    asc = false;
                }
                this.Sort(temp[0], asc);
            }
        }

        public void Sort(string propertyName, bool ascending)
        {
            if (_propertyName == propertyName && _ascending == ascending)
                _ascending = !ascending;
            else
            {
                _propertyName = propertyName;
                _ascending = ascending;
            }
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            PropertyComparer<T> pc = new PropertyComparer<T>(propertyName, (SortDirection)((_ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending));
            this.Sort(pc);
        }

        /// <summary>
        /// Converte a coleção atual em data table
        /// </summary>
        /// <param name="nome">Nome da Tabela</param>
        /// <param name="dominio">Domínio/Namespace</param>
        /// <returns>Objeto DataTable</returns>
        public DataTable ToDataTable(String nome, String dominio)
        {
            DataTable dtt = new DataTable(nome, dominio);

            Type to = this[0].GetType();

            PropertyInfo[] opi = to.GetProperties();

            foreach (PropertyInfo pi in opi)
            {
                dtt.Columns.Add(pi.Name);
            }

            foreach (object obj in this)
            {
                DataRow dr = dtt.NewRow();

                foreach (DataColumn dtc in dtt.Columns)
                {
                    try
                    {
                        dr[dtc.ColumnName] = obj.GetType().GetProperty(dtc.ColumnName).GetValue(obj, null).ToString();
                    }
                    catch (Exception ex)
                    { 
                    }
                }
                dtt.Rows.Add(dr);
            }
            return dtt;
        }

        /// <summary>
        /// Importa todos os dados de um List T
        /// </summary>
        /// <param name="obj">Objeto de lista original</param>
        public void ImportFromList(List<T> obj)
        {
            this.AddRange(obj.ToArray());
        }
    }

    public class PropertyComparer<T> : IComparer<T>
    {
        private PropertyInfo property;
        private SortDirection sortDirection;

        public PropertyComparer(string sortProperty, SortDirection sortDirection)
        {
            property = typeof(T).GetProperty(sortProperty.ToString());
            this.sortDirection = sortDirection;
        }

        public int Compare(T x, T y)
        {
            object valueX = property.GetValue(x, null);
            object valueY = property.GetValue(y, null);

            //*******************************************************
            // Bloco abaixo tenta converter para data a propriedade
            // para que seja feita a ordenação por data
            //*******************************************************
            try
            {
                valueX = Convert.ToDateTime(valueX);
            }
            catch
            { 
            }

            try
            {
                valueY = Convert.ToDateTime(valueY);
            }
            catch
            { 
            }
            //*******************************************************

            if (sortDirection == SortDirection.Ascending)
            {
                try
                {
                    return Comparer.Default.Compare(valueX, valueY);
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                try
                {
                    return Comparer.Default.Compare(valueY, valueX);
                }
                catch
                {
                    return 0;
                }
            }
        }
    }
}
