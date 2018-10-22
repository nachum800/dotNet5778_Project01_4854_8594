﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class DSxml
    {
        private static string solutionDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;

        private static string filePath = System.IO.Path.Combine(solutionDirectory, "DS", "DataXML");

        private static XElement NannyRoot = null;
        private static string Nannypath = Path.Combine(filePath, "NannyXml.xml");

        private static XElement motherRoot = null;
        static string motherPath = Path.Combine(filePath, "MotherXml.xml");

        private static XElement ChildRoot = null;
        private static string ChildPath = Path.Combine(filePath, "ChildXml.xml");

        private static XElement contractRoot = null;
        static string contractPath = Path.Combine(filePath, "ContractXml.xml");

        static DSxml()
        {
            bool exists = Directory.Exists(filePath);
            if (!exists)
            {
                Directory.CreateDirectory(filePath);
            }
           

            if (!File.Exists(motherPath))
            {
                CreateFile("Mothers", motherPath);

            }
            else
            { 
             motherRoot = LoadData(motherPath);
            }

            if (!File.Exists(Nannypath))
            {
                CreateFile("Nannies",Nannypath);
            }
            else
            {
                NannyRoot=LoadData(Nannypath);
            }

            if (!File.Exists(ChildPath))
            {
                CreateFile("Children",ChildPath);
            }
            else
            {
                ChildRoot = LoadData(ChildPath);
            }


            if (!File.Exists(contractPath))
            {
                CreateFile("Contracts", contractPath);
            }
            else
            {
                contractRoot = LoadData(contractPath);
            }
        }

        public static void Save(XElement root, string path)
        {
            root.Save(path);
        }

        public static void SaveMothers()
        {
            motherRoot.Save(motherPath);
        }

        public static void SaveContracts()
        {
            contractRoot.Save(contractPath);
        }

        public static void SaveNannies()
        {
            NannyRoot.Save(Nannypath);
        }

        public static void SaveChildren()
        {
           ChildRoot.Save(ChildPath); 
        }

        public static XElement Mothers
        {
            get
            {
                motherRoot = LoadData(motherPath);
                return motherRoot;
            }
        }

        

        public static XElement Contracts
        {
            get
            {
                contractRoot = LoadData(contractPath);
                return contractRoot;
            }
        }

        public static XElement Nannies
        {
            get
            {
                NannyRoot = LoadData(Nannypath);
                return NannyRoot;
            }
        }

        public static XElement Children
        {
            get
            {
                ChildRoot = LoadData(ChildPath);
                return ChildRoot;
            }
        }

        private static void CreateFile(string typename, string path)
        {
            XElement root = new XElement(typename);
            root.Save(path);
        }

        private static XElement LoadData(string path)
        {
            XElement root;
            try
            {
                root = XElement.Load(path);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
            return root;
        }
    }
}

