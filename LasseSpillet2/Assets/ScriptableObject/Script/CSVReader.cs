using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

public class CSVReader
{
	static string SPLIT_RE = ",";
	static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static char[] TRIM_CHARS = { ' ', '\"' };

	public static Dictionary<string, List<string>> Read()
	{
		var list = new List<Dictionary<string, object>>();
		TextAsset data = Resources.Load("CSV") as TextAsset;

		var lines = Regex.Split(data.text, LINE_SPLIT_RE);

		var header = Regex.Split(lines[0], SPLIT_RE);
		List<string>[] listArray = new List<string>[header.Length];
        for (int i = 0; i < listArray.Length; i++)
        {
			listArray[i] = new List<string>();
        }
		for (var i = 1; i < lines.Length; i++)
		{

			var values = Regex.Split(lines[i], SPLIT_RE);
			if (values.Length == 0 || values[0] == "") continue;
			for (var j = 0; j < header.Length && j < values.Length; j++)
			{
				string value = values[j];
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace('\\', ' ');
				object finalvalue = value;
				listArray[j].Add(value);
				//entry[header[j]] = finalvalue;
			}
			//list.Add(entry);
		}
		var dicList = new Dictionary<string, List<string>>();
		int x = 0;
        foreach (var item in header)
        {
			var sortlist = listArray[x];
            for (int i = 0; i < listArray[x].Count; i++)
            {
				if (listArray[x][i] == "null")
                {
					sortlist.RemoveRange(i,listArray[x].Count- i);
					break;
                }
			}
			dicList.Add(item, sortlist);
			x++;
        }
		return dicList;
	}
}