using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;
using System.Data;

namespace GroupOnC2
{
	public class XMLHelper
	{
		string filePath = "";
		public XMLHelper(string duongDan)
		{
			filePath = duongDan;
		}
		public List<string> LayAnhBia()
		{
			return LayDuongDan(1);
		}
		public List<string> LayAnhChiTiet()
		{
			return LayDuongDan(0);
		}
		public List<string> LayDuongDan(int type)
		{
			List<string> strAnhbia = new List<string>();
			XDocument doc = XDocument.Load(filePath);
			var imgs = from img in doc.Descendants("IMAGE") 
					   where img.Attribute("type").Value.Equals(type.ToString())
					   select new
					   {
						   type = Convert.ToInt32(img.Attribute("type").Value),
						   src = img.Attribute("src").Value
					   };
			foreach (var i in imgs)
			{
				string src = i.src;
				strAnhbia.Add(src);
			}
			return strAnhbia;
		}
	}
}