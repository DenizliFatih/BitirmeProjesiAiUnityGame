using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Set DateTime")]
    [Description("Changes the value of a string to current DateTime")]
    [Image(typeof(IconString), ColorTheme.Type.Yellow)]

    [Version(0, 0, 1)]

    [Category("Math/Text/Set DateTime")]
    [Parameter("DateTime", "The source of the DateTime")]

    [Serializable]
    public class InstructionTextSetDateTime : TInstructionText
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Set {this.m_Set} = Current DateTime";

        // RUN METHOD: ----------------------------------------------------------------------------

        protected override Task Run(Args args)
        {
            // Get current local DateTime
            string currentDateTime = DateTime.Now.ToString();

            this.m_Set.Set(currentDateTime, args);

            return DefaultResult;
        }
    }
}
