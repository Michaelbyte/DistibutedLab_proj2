using System.Numerics;
using System.Text;

var bytesBinary = GetBytesBinary("FF00000000000000000000000000000000000000000000000000000000000000");

BigInteger totalValue = 0;

bytesBinary.Reverse();

for (int i = 0; i < bytesBinary.Count; i++)
{
    var byteValue = 0;
    var currentByte = bytesBinary[i];
    
    for (int j = 0; j < currentByte.Length; j++)
    {
        if (currentByte[j] == '0')
            continue;
        byteValue += (int)Math.Pow(2, j);
    }

    totalValue += byteValue * BigInteger.Pow(256,i);
}

Console.WriteLine(totalValue);

Console.ReadKey();

static List<string> Calculate(List<string> s, int num)
{
    var buffer = new List<string>();

    Action<string> addTop = (string first) =>
    {
        var sb = new StringBuilder();

        for (int i = 0; i < s.Count; i++)
        {
            var part = sb.AppendJoin("", first, s[i]).ToString();
            sb.Clear();
            buffer.Add(part);
        }
    };

    addTop("0");
    addTop("1");

    s.Clear();

    s = s.Concat(buffer).ToList();

    return s.Count < num ? Calculate(s, num) : s;
}

static List<string> GetBytesBinary(string input)
{
    var bytesAmount = input.Length / 2;
    var bytes = new List<string>();
    var bytesBinary = new List<string>();

    for (int i = 0; i < bytesAmount; i++)
    {
        var byteBinary = "";
        var @byte = input[i..(i + 2)];
        bytes.Add(@byte);
        byteBinary += FromHexToBinary(@byte[0]);
        byteBinary += FromHexToBinary(@byte[1]);
        bytesBinary.Add(byteBinary);
    }

    //foreach (var byteBinary in bytesBinary)
    //    Console.WriteLine(byteBinary);

    return bytesBinary;
}

static string FromHexToBinary(char value)
{
    var template = new List<string>()
    {
        "00",
        "01",
        "10",
        "11"
    };

    var permutations = Calculate(template, 16);

    for (int i = 0; i < permutations.Count; i++)
    {
        var res = 0;

        for (int j = 0; j < permutations[i].Length; j++)
        {
            if (permutations[i][j] == '0')
                continue;
            res += (int)Math.Pow(2, j);
        }

        var halfByteHex = res < 10 ? Convert.ToString(res) : Convert.ToString((char)(res + 55));

        if (halfByteHex == Convert.ToString(value))
            return permutations[i];
    }

    return null;
}