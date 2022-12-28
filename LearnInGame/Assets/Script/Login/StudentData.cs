[System.Serializable]
public class StudentData
{
    private static string student_id;
    public static void setStudentId(string id)
    {
        student_id = id;
    }
    public static string getStudentId()
    {
        return student_id;
    }
}
