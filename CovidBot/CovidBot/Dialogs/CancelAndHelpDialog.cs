// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.6.2

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace CovidBot.Dialogs
{
    public class CancelAndHelpDialog : ComponentDialog
    {
        private const string HelpMsgText = "How can i help you \n I can diagonse you for covid";
        private const string CancelMsgText = "Cancelling...";

        public CancelAndHelpDialog(string id)
            : base(id)
        {
        }

        protected override async Task<DialogTurnResult> OnContinueDialogAsync(DialogContext innerDc, CancellationToken cancellationToken = default)
        {
            var result = await InterruptAsync(innerDc, cancellationToken);
            if (result != null)
            {
                return result;
            }

            return await base.OnContinueDialogAsync(innerDc, cancellationToken);
        }

        private async Task<DialogTurnResult> InterruptAsync(DialogContext innerDc, CancellationToken cancellationToken)
        {
            if (innerDc.Context.Activity.Type == ActivityTypes.Message)
            {
                if (innerDc.Context.Activity.Text != null)
                {
                    var text = innerDc.Context.Activity.Text.ToLowerInvariant();

                    switch (text)
                    {
                        case "help":
                        case "?":
                            var helpMessage = MessageFactory.Text(HelpMsgText, HelpMsgText, InputHints.ExpectingInput);
                            await innerDc.Context.SendActivityAsync(helpMessage, cancellationToken);
                            return new DialogTurnResult(DialogTurnStatus.Waiting);

                        case "cancel":
                        case "quit":
                            var cancelMessage = MessageFactory.Text(CancelMsgText, CancelMsgText, InputHints.IgnoringInput);
                            await innerDc.Context.SendActivityAsync(cancelMessage, cancellationToken);
                            return await innerDc.CancelAllDialogsAsync(cancellationToken);
                        case "thank you":
                        case "Thank you":
                        case "Bye":
                        case "bye":
                            var thankMessage = MessageFactory.Text("Thank You", "Thank You", InputHints.IgnoringInput);
                            await innerDc.Context.SendActivityAsync(thankMessage, cancellationToken);
                            return await innerDc.CancelAllDialogsAsync(cancellationToken);
                    }
                }
            }

            return null;
        }
    }
}
