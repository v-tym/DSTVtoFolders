using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using Tekla.Structures;
using Tekla.Structures.Model;
using TSMO=Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;




using System.Collections;


namespace DSTVtoFolders
{
    public class MyPlate
    {
        public string PlateProfile { get; set; }
        public string PlateMaterial { get; set; }

        public void Add(string PlateProfile, string PlateMaterial)
        {

            this.PlateProfile = PlateProfile;
            this.PlateMaterial = PlateMaterial;


        }

    }


    public class MyPlateComparer : IEqualityComparer<MyPlate>
    {



        public bool Equals(MyPlate x, MyPlate y)
        {

            return x.PlateMaterial == y.PlateMaterial
                   &&
                   x.PlateProfile == y.PlateProfile;


        }


        public int GetHashCode(MyPlate mp)
        {

            unchecked
            {

                return mp.PlateMaterial.GetHashCode() ^
                       mp.PlateProfile.GetHashCode();

            }


        }



    }


    //public class ClassLogic
    //{



    //}
}
