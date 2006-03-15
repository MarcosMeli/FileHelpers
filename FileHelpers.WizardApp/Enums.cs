using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.WizardApp
{
    public enum NetLenguage
    {
        VbNet,
        CSharp
    }

    public enum NetVisibility
    {
        Private,
        Protected,
        Public,
        Internal
    }

    public enum RecordKind
    {
        FixedLength,
        Delimited
    }

    public class EnumHelper
    {
        public static string GetVisibility(NetLenguage leng, NetVisibility visi)
        {
            switch (leng)
            {
                case NetLenguage.VbNet:
                    switch (visi)
                    {
                        case NetVisibility.Private:
                            return "Private";
                        case NetVisibility.Protected:
                            return "Protected";
                        case NetVisibility.Public:
                            return "Public";
                        case NetVisibility.Internal:
                            return "Friend";
                    }
                    break;
                case NetLenguage.CSharp:
                    switch (visi)
                    {
                        case NetVisibility.Private:
                            return "private";
                        case NetVisibility.Protected:
                            return "protected";
                        case NetVisibility.Public:
                            return "public";
                        case NetVisibility.Internal:
                            return "internal";
                    }
                    break;
                default:
                    break;
            }

            return "";
        }
    }

}
