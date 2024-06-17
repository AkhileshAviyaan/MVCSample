using Conversion.Parameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion.Seeding
{
	public class Seeding
	{
		public List<EllipsoidalConstant> ecList;
		public List<ProjectionConstant> pcList;

		public Seeding()
		{
			EllipsoidalConstant airy1830 = new(6377563.396, 6356256.909,"Airy1830");
			EllipsoidalConstant grs80 = new(6378137.00, 6356752.3141,"Grs80");
			EllipsoidalConstant Everest1830 = new() { a = 6377276.345, f = 1 / 300.8017,ECName="Everest1830" };
			EllipsoidalConstant Wgs84 = new() { a = 6378137, f = 1 / 298.257223563,ECName="Wgs84" };


			ProjectionConstant nationalGrid = new(-100000, 400000, 0.9996012717, 49, -2,"National Grid");
			ProjectionConstant Utm44 = new(0, 500000, 0.9996, 0, 81,"Utm44");
			ProjectionConstant Utm45 = new(0, 500000, 0.9996, 0, 87, "Utm45");
			ProjectionConstant Mutm81 = new(0, 500000, 0.9999, 0, 81, "Utm81");
			ProjectionConstant Mutm84 = new(0, 500000, 0.9999, 0, 84, "Utm84");
			ProjectionConstant Mutm87 = new(0, 500000, 0.9999, 0, 87, "Utm87");
			ecList = [airy1830,grs80,Everest1830,Wgs84];
			pcList = [nationalGrid,Utm44,Utm45,Mutm81,Mutm84,Mutm87];

		}

	}
}
