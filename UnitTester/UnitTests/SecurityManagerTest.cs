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
using ExpressUnitModel;
using ExpressUnit;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class SecurityManagerTest
    {
        private string xml;

        public SecurityManagerTest()
        {
            xml = @"<root>
                        <main>
                            <test1>test1</test1>
                            <test2>test2</test2>
                        </main>
                    </root>";
        }

        [UnitTest]
        public void SecurityManager_CreateMockX509Certificate_Will_Create_Mock_Certificate_Test()
        {
            SecurityManager manager = new SecurityManager();
            MockX509Certificate cert = manager.CreateMockX509Certificate();

            Confirm.Different(null, cert.PrivateKey);
            Confirm.Different(null, cert.PublicKey);
        }

        [UnitTest]
        public void Test_That_Mock_Certificate_Can_Encrypt_Document_Test()
        {
            SecurityManager manager = new SecurityManager();
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);

            MockX509Certificate cert = manager.CreateMockX509Certificate();

            Encrypt(doc, "main" ,cert.PublicKey);
        }

        [UnitTest]
        public void Test_That_Mock_Certificate_Can_Decrypt_Document_Test()
        {
            SecurityManager manager = new SecurityManager();
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);

            MockX509Certificate cert = manager.CreateMockX509Certificate();

            Encrypt(doc, "main", cert.PublicKey);

            Decrypt(doc, cert.PrivateKey);
        }

        [UnitTest]
        public void Test_That_Mock_Certificate_Can_Sign_And_Verify_Xml_Signature_Test()
        {
            SecurityManager manager = new SecurityManager();
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);
            
            MockX509Certificate cert = manager.CreateMockX509Certificate();
            SignXml(doc, cert.PrivateKey);
        
            Confirm.Equal(true,VerifyXml(doc,cert.PublicKey));
        }

        [UnitTest]
        public void Test_That_Mock_Certificate_Can_Reject_Xml_That_Has_Been_Modified_After_Signing_Test()
        {
            SecurityManager manager = new SecurityManager();
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);

            MockX509Certificate cert = manager.CreateMockX509Certificate();

            SignXml(doc, cert.PrivateKey);

            doc.GetElementsByTagName("test1")[0].InnerText = "change after signing";

            Confirm.Equal(false, VerifyXml(doc, cert.PublicKey));
        }

        public static void Encrypt(XmlDocument Doc, string ElementToEncrypt, AsymmetricAlgorithm publicKey)
        {
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;
         
            EncryptedXml eXml = new EncryptedXml();
            eXml.AddKeyNameMapping("encKey", publicKey);
           
            EncryptedData edElement = eXml.Encrypt(elementToEncrypt, "encKey");
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }


        public void SignXml(XmlDocument Doc, AsymmetricAlgorithm privateKey)
        {
            SignedXml signedXml = new SignedXml(Doc);
            signedXml.SigningKey = privateKey;

            Reference reference = new Reference();
            reference.Uri = "";

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            signedXml.AddReference(reference);

            signedXml.ComputeSignature();

            XmlElement xmlDigitalSignature = signedXml.GetXml();
            Doc.DocumentElement.AppendChild(Doc.ImportNode(xmlDigitalSignature, true));
        }

        public bool VerifyXml(XmlDocument Doc, AsymmetricAlgorithm publicKey)
        {
            SignedXml signedXml = new SignedXml(Doc);
            XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

            signedXml.LoadXml((XmlElement)nodeList[0]);

            return signedXml.CheckSignature(publicKey);
        }

        public static void Decrypt(XmlDocument Doc,AsymmetricAlgorithm privateKey)
        {
            EncryptedXml exml = new EncryptedXml(Doc);
            exml.AddKeyNameMapping("encKey", privateKey);
            exml.DecryptDocument();
        }
    }
}
