﻿using Experimental.System.Messaging;

namespace CommonLayer.MSMQService
{
    public class MessageSender
    {
        /// <summary>
        /// SMTP service to send message through 
        /// messageQueue
        /// </summary>
        /// <param name = "MessageToBeSend" ></ param >
        public void Message(string emailMessage)
        {
            MessageQueue MyQueue;
            if (MessageQueue.Exists(@".\Private$\myqueue"))
            {
                MyQueue = new MessageQueue(@".\Private$\myqueue");
            }
            else
            {
                MyQueue = MessageQueue.Create(@".\Private$\myqueue");
            }
            Message MyMessage = new Message();
            MyMessage.Formatter = new BinaryMessageFormatter();
            MyMessage.Body = emailMessage;
            MyMessage.Label = "Registration";
            MyMessage.Priority = MessagePriority.Normal;
            MyQueue.Send(MyMessage);
        }
    }
}
