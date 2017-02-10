using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    abstract public class EntityObject
    {
		static private int _compteur = 0;
		public int ID { get; set; }

		public EntityObject() {
			ID = _compteur++;
		}

		static public int get_compteur() {
			return _compteur;
		}
    }
}
