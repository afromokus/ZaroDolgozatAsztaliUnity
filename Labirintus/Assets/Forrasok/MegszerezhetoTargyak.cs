using System.Collections.Generic;

public class MegszerezhetoTargyak
{
    List<string> targyNevek = new List<string>();


    public MegszerezhetoTargyak()
    {
        feltoltTargynevekLista();
    }

    private void feltoltTargynevekLista()
    {
        targyNevek.Add("Csont");
        targyNevek.Add("Kutya");
        targyNevek.Add("Kövek");
        targyNevek.Add("Vágókesztyű");
        targyNevek.Add("Búza");
        targyNevek.Add("Vödör");
        targyNevek.Add("Tejes vödör");
        targyNevek.Add("Kulcs fémtokja");
    }

    public List<string> TargyNevek { get => targyNevek; set => targyNevek = value; }


}
