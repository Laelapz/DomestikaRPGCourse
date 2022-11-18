using ScriptableObjectArchitecture;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public ConversationSO conversation;

    [Header("Broadcasting Events")]
    public ConversationSOGameEvent conversationRequestEvent;


    public void TriggerConversation()
    {
        this.conversationRequestEvent.Raise(this.conversation);
    }
}
