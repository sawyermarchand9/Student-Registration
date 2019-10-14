using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO; //file system 
using System.Collections;
using System.Data;

namespace Student_Registration
{
    public class XMLAccess
    {
        #region Properties
        private string StudentXmlPath
        {
            get
            {
                return ConfigurationManager.AppSettings["StudentXML"];
            }
        }
        private string InstructorXmlPath
        {
            get
            {
                return ConfigurationManager.AppSettings["InstructorXML"];
            }
        }
        private string CourseXmlPath
        {
            get
            {
                return ConfigurationManager.AppSettings["CourseXML"];
            }
        }
        #endregion

        #region Ctor

        public XMLAccess()
        {


        }
        #endregion

        #region Methods

        public bool AddNode(Dictionary<string, string> studentInfo)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(StudentXmlPath);

                //Create a new student node with XmlNode class
                XmlNode newStudent = xmlDocument.CreateNode(XmlNodeType.Element, "Student", null);

                //Creat new student info nodes with XmlElement class
                foreach (KeyValuePair<string, string> entry in studentInfo)
                {
                    XmlElement node = xmlDocument.CreateElement(entry.Key.ToString());
                    node.InnerText = entry.Value.ToString();

                    //add nodes to the new student node
                    newStudent.AppendChild(node);
                }


                //Add the node to the document.
                XmlNode root = xmlDocument.DocumentElement;
                root.AppendChild(newStudent);

                xmlDocument.Save(StudentXmlPath);

                return true;
            }
            catch
            {
                return false; 
            }
        }

        public bool RemoveNode(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(StudentXmlPath);

            XmlNode node = xmlDocument.SelectSingleNode(String.Format("Students/Student[name='{0}']", name));
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);

                xmlDocument.Save(StudentXmlPath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SearchNode(String name, string pwd)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(StudentXmlPath);

            XmlNode node = xmlDocument.SelectSingleNode(String.Format("Students/Student[name='{0}' and password='{1}']", name, pwd));
            if (node != null)
                return true;
            else
                return false;

        }

        public Hashtable GetNodesByName(String name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(StudentXmlPath);

            Hashtable hashtable = new Hashtable();

            XmlNode node = xmlDocument.SelectSingleNode(String.Format("Students/Student[name='{0}']", name));
            if (node != null)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    hashtable.Add(child.Name, child.InnerText);
                }

                return hashtable;
            }

            else
                return null;

        }

        public DataSet RetrieveNodes()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(StudentXmlPath);

            return ds;
        }
        #endregion
    }
}