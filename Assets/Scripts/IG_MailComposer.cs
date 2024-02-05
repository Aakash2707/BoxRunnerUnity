using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;

public class IG_MailComposer : MonoBehaviour
{

    // Update is called once per frame
    public void SendTextMail()
    {
        bool canSendMail = MailComposer.CanSendMail();
        if(canSendMail){
            MailComposer composer = MailComposer.CreateInstance();
            composer.SetToRecipients(new string[1]{"to@gmail.com"});

            composer.SetSubject("Play Box Runner");
            composer.SetBody("Challenge An endless Runner Game to unlock Skins and create High score", false);//Pass true if string is html content
            composer.SetCompletionCallback((result, error) => {
            Debug.Log("Mail composer was closed. Result code: " + result.ResultCode);});
            composer.Show();    
        }
        
    }
}
