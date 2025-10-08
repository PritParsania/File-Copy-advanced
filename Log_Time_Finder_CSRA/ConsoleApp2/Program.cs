using ConsoleApp2;

internal class Program
{
    private static void Main()
    {
        string location = $"C:\\Users\\Admin\\Downloads\\BeforeLog-AllInOne\\BeforeLog-AllInOne\\10";

        string[] arrInputFiles = Directory.GetFiles(location);

        string StartTime = string.Empty;
        string EndTime = string.Empty;
        int sEndTimeMilliSeconds = 0;
        int sStartTimeMilliSeconds = 0;
        List<int> TotalTime = new List<int>();

        List<Time> time = new List<Time>();

        foreach (string sFile in arrInputFiles)
        {
            string[] arrLines = File.ReadAllLines(sFile);

            foreach (string sLine in arrLines)
            {

                Time objTime = new Time();

                if (sLine.Contains($"[START] KmGetDeviceInfo API."))
                {
                    string[] arrLine = sLine.Split(' ');
                    string[] arrTime = arrLine[1].Split(',');
                    string[] arrTimeMilliSeconds = arrTime[1].Split('\t');
                    int.TryParse(arrTimeMilliSeconds[0], out sStartTimeMilliSeconds);
                    StartTime = arrTime[0];
                }

                if (sLine.Contains($"[END] KmGetDeviceInfo API."))
                {
                    string[] arrLine = sLine.Split(' ');
                    string[] arrTime = arrLine[1].Split(',');
                    string[] arrTimeMilliSeconds = arrTime[1].Split('\t');
                    int.TryParse(arrTimeMilliSeconds[0], out sEndTimeMilliSeconds);
                    EndTime = arrTime[0];
                }

                if (StartTime != string.Empty && EndTime != string.Empty)
                {
                    objTime.StartTime = StartTime;
                    objTime.EndTime = EndTime;
                    time.Add(objTime);

                    TimeSpan starttime = TimeSpan.Parse(StartTime);
                    string? sExecutionStartTime = $"{starttime},{sStartTimeMilliSeconds}";

                    TimeSpan endtime = TimeSpan.Parse(EndTime);
                    string? sExecutionEndTime = $"{endtime},{sEndTimeMilliSeconds}";

                    TimeSpan ElapsedMilliseconds = endtime.Subtract(starttime);
                    int iTime = Convert.ToInt32(ElapsedMilliseconds.TotalMilliseconds) + (sEndTimeMilliSeconds - sStartTimeMilliSeconds);

                    TotalTime.Add(iTime);

                    StartTime = string.Empty;
                    EndTime = string.Empty;
                }

            }
        }
        foreach (var s in TotalTime)
        {
            Console.WriteLine(s);
        }
        //}
    }
}