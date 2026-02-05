using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Patient
{
    public string name;
    public string surname;
    public string lastName;
    public string gender;
    public int age;
    public DateTime birthday;
    public int height;
    public int weight;

    public Patient(string _name, string _surname, string _lastName, int checkGender, DateTime _birthday, int _height, int _weight)
    {
        name = _name;
        surname = _surname;
        lastName = _lastName;
        if (checkGender == 0)
        {
            gender = "Мужской";
        }
        else
        {
            gender = "Женский";
        }
        age = DateTime.Now.Year - _birthday.Year;
        birthday = _birthday;
        height = _height;
        weight = _weight;
    }
}
