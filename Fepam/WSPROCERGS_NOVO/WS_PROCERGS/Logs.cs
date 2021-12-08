using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WS_PROCERGS
{
    /// <summary>
    /// Classe para regras de Logs
    /// </summary>
    public class Logs
    {
        #region Fields
        private static List<Object> _objects = new List<object>();
        private static String _description = "";
        private static String _destinationpath = "";
        #endregion

        #region Properties
        public static List<Object> Objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;
            }
        }
        public static String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Logs()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Escreve o log no disco
        /// </summary>
        public static void WriteLog()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("--------------");

                sb.AppendLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                _description = "> " + _description;
                sb.AppendLine(_description);

                foreach (Object o in _objects)
                {
                    sb.AppendLine(SerializeObject(o));
                    sb.AppendLine("--------------");
                }

                if (_destinationpath == string.Empty)
                {
                    _destinationpath = @"C:\PROJETOS\WS_PROCERGS\WS_PROCERGS\Logs";

                }

                if (_destinationpath.EndsWith(@"\") != true && !File.Exists(_destinationpath))
                {
                    _destinationpath += @"\";

                    _destinationpath += @"log_" + DateTime.Now.ToString("yyyyMMdd") +".txt";
                }

                TextWriter tw = new StreamWriter(_destinationpath, true, Encoding.Default);
                tw.WriteLine(sb.ToString());
                tw.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Escreve log de uma exception
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void WriteLog(Exception ex)
        {
            if (ex != null)
            {
                StringBuilder sb = new StringBuilder();
                WriteLog(ex, ref sb);
                _description += sb.ToString();
                WriteLog();
            }
        }

        /// <summary>
        /// Escreve log de uma exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="description">Descrição de erro</param>
        public static void WriteLog(Exception ex, String description)
        {
            if (ex != null && !string.IsNullOrEmpty(description))
            {
                _description = description;
                WriteLog(ex);
            }
        }

        /// <summary>
        /// Escreve log de uma exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="sb">StringBuilder</param>
        private static void WriteLog(Exception ex, ref StringBuilder sb)
        {
            try
            {
                if (ex != null)
                {
                    if (sb == null)
                    {
                        sb = new StringBuilder();
                    }
                    sb.AppendLine(ex.Message);
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine(ex.Source);
                    WriteLog(ex.InnerException, ref sb);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        public static void WriteLog(String description)
        {
            _description = description;
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="destination">Diretório de destino</param>
        /// <example>C:\</example>
        public static void WriteLog(String description, String destination)
        {
            _destinationpath = destination;
            _description = description;
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="iobject">Objeto qualquer</param>
        public static void WriteLog(String description, Object iobject)
        {
            _objects.Clear();
            _description = description;
            _objects.Add(iobject);
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="iobject">Objeto qualquer</param>
        /// <param name="destination">Diretório de destino</param>
        /// <example>C:\</example>
        public static void WriteLog(String description, Object iobject, String destination)
        {
            _objects.Clear();
            _destinationpath = destination;
            _description = description;
            _objects.Add(iobject);
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="objects">Coleção de objetos</param>
        public static void WriteLog(String description, List<Object> objects)
        {
            _objects.Clear();
            _description = description;
            _objects = objects;
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="objects">Coleção de objetos</param>
        /// <param name="destination">Diretório de destino</param>
        /// <example>C:\</example>
        public static void WriteLog(String description, List<Object> objects, String destination)
        {
            _objects.Clear();
            _destinationpath = destination;
            _description = description;
            _objects = objects;
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="objects">ArrayList de objetos</param>
        public static void WriteLog(String description, ArrayList objects)
        {
            _objects.Clear();
            _description = description;
            foreach (Object obj in objects)
            {
                _objects.Add(obj);
            }
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="description">Descrição do log</param>
        /// <param name="objects">ArrayList de objetos</param>
        /// <param name="destination">Diretório de destino</param>
        /// <example>C:\</example>
        public static void WriteLog(String description, ArrayList objects, String destination)
        {
            _objects.Clear();
            _destinationpath = destination;
            _description = description;
            foreach (Object obj in objects)
            {
                _objects.Add(obj);
            }
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="iobject">Objeto qualquer</param>
        public static void WriteLog(Object iobject)
        {
            _objects.Clear();
            _objects.Add(iobject);
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="objects">Coleção de objetos</param>
        public static void WriteLog(List<Object> objects)
        {
            _objects.Clear();
            _objects = objects;
            WriteLog();
        }

        /// <summary>
        /// Escreve log no disco
        /// </summary>
        /// <param name="objects">ArrayList de objetos</param>
        public static void WriteLog(ArrayList objects)
        {
            _objects.Clear();
            foreach (Object obj in objects)
            {
                _objects.Add(obj);
            }
            WriteLog();
        }

        /// <summary>
        /// Converte u Byte Array de Unicode values (UTF-8 encoded) para uma String completa.
        /// </summary>
        /// <param name="characters">Unicode Byte Array que serão convertidos para String</param>
        /// <returns>String convertida de Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString);

        }

        /// <summary>
        /// Serializa um objeto
        /// </summary>
        /// <param name="pObject">Objeto que será serializado</param>
        /// <returns>String contendo o objeto serializado</returns>
        private static String SerializeObject(Object pObject)
        {

            try
            {

                String XmlizedString = null;

                MemoryStream memoryStream = new MemoryStream();

                XmlSerializer xs = new XmlSerializer(pObject.GetType());

                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);

                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;

                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());

                return XmlizedString;

            }
            catch (Exception e)
            {
                return "Erro ao serializar objeto: " + e.Message;
            }
        }
        #endregion
    }
}