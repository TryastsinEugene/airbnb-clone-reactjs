using RealEstate.DLL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.BLL.Interfaces
{
	public interface ITokenService
	{
		Task<string> CreateToken(User user);
	}
}
