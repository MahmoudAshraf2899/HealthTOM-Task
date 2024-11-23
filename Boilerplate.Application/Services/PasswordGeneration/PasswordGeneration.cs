using Boilerplate.Contracts.IServices.Services.PasswordGeneration;

namespace Boilerplate.Application.Services.PasswordGeneration;

public class PasswordGenerationService : IPasswordGenerationService
{
    public PasswordGenerationService() { }
    public string Generate()
    {
        string allowedChars = "";

        allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

        allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

        allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
        string numbersChars = "1,2,3,4,5,6,7,8,9,0";
        string nonAlphaChars = "!,@,#,$,%,&,?";
        string lowerAlphaChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string CapitalAlphaChars = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);
        string[] numbersArr = numbersChars.Split(sep);
        string[] nonAlphaArr = nonAlphaChars.Split(sep);
        string[] lowerAlphaArr = lowerAlphaChars.Split(sep);
        string[] CapitalAlphaArr = CapitalAlphaChars.Split(sep);

        string passwordString = "";

        Random rand = new Random();

        for (int i = 0; i < 8; i++)
        {
            if (i == 1) passwordString += numbersArr[rand.Next(0, numbersArr.Length)];
            else if (i == 3) passwordString += CapitalAlphaArr[rand.Next(0, CapitalAlphaArr.Length)];
            else if (i == 5) passwordString += lowerAlphaArr[rand.Next(0, lowerAlphaArr.Length)];
            else if (i == 6) passwordString += nonAlphaArr[rand.Next(0, nonAlphaArr.Length)];
            else passwordString += arr[rand.Next(0, arr.Length)];
        }
        return passwordString;
    }
}