using Conversion.Parameter;
using Conversion.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conversion.Utility.MathHelp;
using Conversion.Transform;

namespace Conversion.ConversionMethod
{
	public class LSM
	{
		public Matrix A;
		public Matrix AColumnMatrix;
		public Matrix BColumnMatrix;
		public Matrix B;

		public Matrix DelBA;
		Matrix AArranged;
		public SevenParameter SevenParameters;
		public Matrix SevenParameterInMatrix=new Matrix(7,1);
		public LSM(List<XyzParameter> xyzA, List<XyzParameter> xyzB) : this(xyzA)
		{
			BColumnMatrix = new Matrix(xyzB.Count * 3, 1);
			DelBA = new Matrix(xyzB.Count * 3, 1);

			for (int i = 0; i < xyzB.Count; i++)
			{
				BColumnMatrix.Data[0 + i * 3, 0] = xyzB[i].X;
				BColumnMatrix.Data[1 + i * 3, 0] = xyzB[i].Y;
				BColumnMatrix.Data[2 + i * 3, 0] = xyzB[i].Z;
				DelBA.Data[0 + i * 3, 0] = xyzB[i].X - xyzA[i].X;
				DelBA.Data[1 + i * 3, 0] = xyzB[i].Y - xyzA[i].Y;
				DelBA.Data[2 + i * 3, 0] = xyzB[i].Z - xyzA[i].Z;
			}
		}

		public LSM(List<XyzParameter> xyz)
		{
			Matrix TempArrange = new Matrix(xyz.Count * 3, 7);
			AColumnMatrix = new Matrix(xyz.Count * 3, 1);
			Matrix TempA = new Matrix(xyz.Count, 3);
			int count = 0;
			for (int i = 0; i < xyz.Count; i++)
			{
				TempA.Data[i, 0] = xyz[i].X;
				TempA.Data[i, 1] = xyz[i].Y;
				TempA.Data[i, 2] = xyz[i].Z;

				AColumnMatrix.Data[0 + i * 3, 0] = xyz[i].X;
				AColumnMatrix.Data[1 + i * 3, 0] = xyz[i].Y;
				AColumnMatrix.Data[2 + i * 3, 0] = xyz[i].Z;

				for (int j = 0; j < 3; j++)
				{

					for (int k = 0; k < 6; k++)
					{
						TempArrange.Data[count, k] = 0;
						if (j == 0)
						{
							TempArrange.Data[count, j] = 1;
							TempArrange.Data[count, 3] = 0;
							TempArrange.Data[count, 4] = xyz[i].Z;
							TempArrange.Data[count, 5] = -xyz[i].Y;
							TempArrange.Data[count, 6] = xyz[i].X;
						}
						else if (j == 1)
						{
							TempArrange.Data[count, j] = 1;
							TempArrange.Data[count, 3] = -xyz[i].Z;
							TempArrange.Data[count, 4] = 0;
							TempArrange.Data[count, 5] = xyz[i].X;
							TempArrange.Data[count, 6] = xyz[i].Y;
						}
						else if (j == 2)
						{
							TempArrange.Data[count, j] = 1;
							TempArrange.Data[count, 3] = xyz[i].Y;
							TempArrange.Data[count, 4] = -xyz[i].X;
							TempArrange.Data[count, 5] = 0;
							TempArrange.Data[count, 6] = xyz[i].Z;
						}
					}
					count++;
				}
			}
			this.AArranged = TempArrange;
			this.A = TempA;
		}

		public Matrix Tranform()
		{
			Matrix Result = AArranged * SevenParameterInMatrix;
			Matrix ArrangedResult = new Matrix(Result.RowCount / 3, 3);

			for (int i = 0; i < Result.RowCount / 3; i++)
			{
				ArrangedResult.Data[i, 0] = Result.Data[0 + i * 3, 0];
				ArrangedResult.Data[i, 1] = Result.Data[1 + i * 3, 0];
				ArrangedResult.Data[i, 2] = Result.Data[2 + i * 3, 0];
			}
			return this.A + ArrangedResult;
		}
		public void CalculateSevenParameter()
		{
			this.SevenParameterInMatrix = (AArranged.Transpose * AArranged).Inverse * (AArranged.Transpose * DelBA);
			MatrixTo7Parameter();
		}
		public void MatrixTo7Parameter()
		{
			var M = this.SevenParameterInMatrix.Data;
			this.SevenParameters = new SevenParameter(M[0,0], M[1, 0], M[2, 0], M[3, 0], M[4, 0], M[5, 0], M[6, 0]);
		}
		public void SevenParameterMatrixUpdate()
		{
			var M = SevenParameters;
			this.SevenParameterInMatrix = new Matrix(new double[7, 1] { {M.Tx},{ M.Ty },{ M.Tz },{ M.Rx },{ M.Ry },{ M.Rz },{ M.S } });
		}
	}
}
