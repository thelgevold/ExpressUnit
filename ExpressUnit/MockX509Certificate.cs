/*
Copyright (C) 2009  Torgeir Helgevold

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Reflection;

namespace ExpressUnit
{
    public class MockX509Certificate : X509Certificate2
    {
        RSACryptoServiceProvider rsaKey;
        public MockX509Certificate()
        {
            CspParameters cspParams = new CspParameters();
            rsaKey = new RSACryptoServiceProvider(cspParams);
        }

        public new AsymmetricAlgorithm PrivateKey 
        {
            get
            {
                RSAParameters par = rsaKey.ExportParameters(true);

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(par);

                return rsa;
            }
        }

        public new AsymmetricAlgorithm PublicKey 
        {
            get
            {
                RSAParameters par = rsaKey.ExportParameters(false);

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(par);

                return rsa;
            }
        }
    }
}
