using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab
{
    public partial class V5MainCollection
    {
        public void Load(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                listV5Data = (List<V5Data>) binaryFormatter.Deserialize(fileStream);
                OnCollectionChanged(binaryFormatter);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error on load: " + e.Message);
                throw e;
            }
            finally
            {
                if (fileStream != null) {
                    fileStream.Close();
                }
            }
        }

        public void Save(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, listV5Data);
                OnCollectionChanged(binaryFormatter);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error on save: " + e.Message);
                throw e;
            }
            finally
            {
                if (fileStream != null) {
                    fileStream.Close();
                }
            }
        }
    }
}
