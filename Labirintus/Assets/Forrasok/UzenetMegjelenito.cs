using UnityEngine.UI;

public class UzenetMegjelenito
{
    int idotart = 0;
    string uzenet = "";
    Text szovegDob;

    public UzenetMegjelenito(int idotart, Text szovegDob)
    {
        this.idotart = idotart;
        this.szovegDob = szovegDob;
    }

    public void megjelenitUzenetet(string uzenet)
    {
        this.uzenet = uzenet;

        szovegDob.text = uzenet;

    }

    #region get set

    public int Idotart
    {
        get
        {
            return idotart;
        }

        set
        {
            idotart = value;
        }
    }

    public string Uzenet
    {
        get
        {
            return uzenet;
        }

        set
        {
            uzenet = value;
        }
    }

    public Text SzovegDob
    {
        get
        {
            return szovegDob;
        }

        set
        {
            szovegDob = value;
        }
    }

#endregion

}
