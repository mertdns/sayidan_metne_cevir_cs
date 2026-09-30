/*
İşlev: kendisine girilen sayının (ör: 200120) "iki yüz bin yüz yirmi" şeklinde metne dökmesidir.
Amaç: vibe coding olmadan algoritma mantığını geliştirmek amacıyla yaptım güzel bir egzersiz oldu.
*/
class Program
{
    static string[] basamaklariUret(string[] degerler, string ifade)
    {
        string[] dizi = new string[degerler.Length - 1];
        dizi[0] = ifade;
        int diziIndex = 0;
        for (int i = 1; i < degerler.Length - 1; i++)
        {
            dizi[++diziIndex] = $"{degerler[i]} {ifade}";
        }
        return dizi;
    }

    static string MetneCevir(string sayi)
    {
        string[] birler = { "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz", "sıfır" };
        string[] onlar = { "on", "yirmi", "otuz", "kırk", "elli", "altmış", "yetmiş", "seksen", "doksan" };
        string[] yuzler = basamaklariUret(birler, "yüz");

        string sonuc = "";

        if (sayi.Length < 1 || sayi.Length > 3)
            return "";

        int basamakSayisi = sayi.Length;

        switch (basamakSayisi)
        {
            case 1:
                if (sayi == "0")
                {
                    sonuc += birler[^1];
                    break;
                }
                sonuc += birler[Convert.ToInt16(sayi) - 1];
                break;
            case 2:
                if(sayi[0].ToString() != "0")
                {
                    sonuc += onlar[Convert.ToInt16(sayi[0].ToString()) - 1];
                }
                if (sayi[1].ToString() != "0")
                {
                    sonuc += " " + birler[Convert.ToInt16(sayi[1].ToString()) - 1];
                }
                break;
            case 3:
                if (sayi[0].ToString() == "0" && sayi[1].ToString() != "0" && sayi[2].ToString() != "0")
                {
                    sonuc += onlar[Convert.ToInt16(sayi[1].ToString()) - 1];
                    sonuc += " " + birler[Convert.ToInt16(sayi[2].ToString()) - 1];
                    break;
                }
                else if (sayi[0].ToString() == "0" && sayi[1].ToString() == "0" && sayi[2].ToString() != "0")
                {
                    sonuc += birler[Convert.ToInt16(sayi[2].ToString()) - 1];
                    break;
                }
                sonuc += yuzler[Convert.ToInt16(sayi[0].ToString()) - 1];
                if (sayi[1].ToString() == "0" && sayi[2].ToString() != "0")
                {
                    sonuc += " " + birler[Convert.ToInt16(sayi[2].ToString()) - 1];
                }
                else if (sayi[1].ToString() != "0")
                {
                    sonuc += " " + onlar[Convert.ToInt16(sayi[1].ToString()) - 1];
                    if (sayi[2].ToString() != "0")
                    {
                        sonuc += " " + birler[Convert.ToInt16(sayi[2].ToString()) - 1];
                    }
                }
                break;
        }

        return sonuc;
    }

    static int EnBuyukKatiniBul(int arananKat, int baslangic)
    {
        for (int i = baslangic; i >= arananKat; i--)
        {
            if (i % arananKat == 0)
                return i;
        }
        return -1;
    }

    static string BolVeIslemiYap(string sayi)
    {
        // 100 200
        string[] ayraclar = { "trilyon", "milyar", "milyon", "bin" };
        List<string> gruplar = new List<string>();
        int uzunluk = sayi.Length;
        string cumle = "";

        if (uzunluk == 0)
            return "";

        if (uzunluk <= 3 && uzunluk >= 1)
            return MetneCevir(sayi);
        
        if(uzunluk % 3 != 0)
        {
            int enBuyukKat = EnBuyukKatiniBul(3, uzunluk);

            if (enBuyukKat != -1)
            {
                string temp = "";
                int index;
                for (index = 0; index < uzunluk - enBuyukKat; index++)
                {
                    temp += $"{sayi[index].ToString()}";
                }

                gruplar.Add(temp);
                temp = null;

                for (int i = index; i < uzunluk; i++)
                {
                    if (i > uzunluk - 2)
                        break;

                    gruplar.Add($"{sayi[i].ToString()}{sayi[i + 1].ToString()}{sayi[i + 2].ToString()}");
                    i += 2;
                }
            }
        }
        else
        {
            for (int i = 0; i < uzunluk; i++)
            {
                if (i > uzunluk - 2)
                    break;

                gruplar.Add($"{sayi[i].ToString()}{sayi[i + 1].ToString()}{sayi[i + 2].ToString()}");
                i += 2;
            }
        }

        int parcaSayisi = gruplar.Count;
        int ayracIndex = -1;

        switch (parcaSayisi)
        {
            case 2:
                ayracIndex = 3;
                break;
            case 3:
                ayracIndex = 2;
                break;
            case 4:
                ayracIndex = 1;
                break;
            case 5:
                ayracIndex = 0;
                break;
            default:
                ayracIndex = -1;
                break;
        }

        if (ayracIndex != -1)
        {
            for (int i = 0; i < gruplar.Count; i++)
            {
                if (i != 0)
                    cumle += " ";

                if (ayracIndex < ayraclar.Length)
                {
                    if (ayracIndex == ayraclar.Length - 1 && MetneCevir(gruplar[i]).Equals("bir"))
                    {
                        cumle += $"{ayraclar[ayracIndex++]}";
                    }
                    else
                    {
                        cumle += $"{MetneCevir(gruplar[i])} {ayraclar[ayracIndex++]}";
                    }
                }
                else
                {
                    cumle += $"{MetneCevir(gruplar[i])}";
                }
            }
        }


        return cumle;
    }

    static void Main()
    {
        Console.WriteLine(BolVeIslemiYap("1221222"));
    }

}
