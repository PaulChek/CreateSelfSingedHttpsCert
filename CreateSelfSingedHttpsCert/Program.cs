using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

#region Subject
Console.WriteLine("Enter domain fore your cert:");
var domainName = Console.ReadLine();
#endregion

#region Months validity
Console.WriteLine("Enter amount of months cert will be valid:");
int.TryParse(Console.ReadLine(), out int months);
#endregion

#region Path
Console.WriteLine($"Enter path for exporting{domainName}.pfx file (users folder by default):");
var path = Console.ReadLine() ?? "~";
Console.WriteLine($"Path will be {path}");
#endregion

#region password
Console.WriteLine($"Enter password for certificate {domainName}.pfx (password by default):");
var password = Console.ReadLine() ?? "password";
#endregion

#region BuisnessLogic
var req = new CertificateRequest($"cn={domainName}", ECDsa.Create(), HashAlgorithmName.SHA256);
var cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddMonths(months));
File.WriteAllBytes(path + $"/{domainName}.pfx", cert.Export(X509ContentType.Pfx, password));
Process.Start("explorer", path);
#endregion