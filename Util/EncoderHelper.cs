namespace StudentCenterWeb.Util;

public static class EncoderHelper
{
    public static string EncodeId(int id)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(id.ToString());
        return Convert.ToBase64String(bytes);
    }

    public static int DecodeId(string encodedId)
    {
        var bytes = Convert.FromBase64String(encodedId);
        return int.Parse(System.Text.Encoding.UTF8.GetString(bytes));
    }
}
