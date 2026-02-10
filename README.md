# SentireChat

# AI-Assisted Chat Mobile App (.NET MAUI)

This project is a mobile chat application built with **.NET MAUI** that integrates with a custom backend API to enable AI-assisted customer support over **WhatsApp**.

Due to limitations of the WhatsApp Web API — which does not allow direct message replies using the registered business number — this application was created to provide a dedicated interface where human attendants can seamlessly take over conversations initiated by an AI assistant.

---

## 🧠 Project Overview

The system works as follows:

1. A customer sends a message via WhatsApp.
2. The message is received by a custom backend API.
3. The API forwards the message to **OpenAI**, which handles the initial automated interaction.
4. The AI responds with information such as:
   - Services offered  
   - Pricing  
   - General business information  
5. When the customer expresses intent to **schedule an appointment**, a **push notification** is sent to the mobile app.
6. A human attendant opens the app, takes over the conversation, and completes the scheduling process directly within the custom chat interface.

This architecture ensures a smooth transition between automated and human-assisted support while respecting WhatsApp API constraints.

---

## 📱 Mobile App Responsibilities

- Receive and display incoming WhatsApp messages
- Show AI-generated responses in real time
- Receive push notifications when human intervention is required
- Allow attendants to take over conversations
- Enable direct chat-based scheduling with customers

---

## 🧩 System Architecture

- **Mobile App:**  
  - .NET MAUI  
  - Custom chat UI  
  - Push notifications  

- **Backend API:**  
  - Handles WhatsApp message ingestion and delivery  
  - Integrates with OpenAI for AI-driven responses  
  - Manages conversation state and handoff logic  

- **External Services:**  
  - WhatsApp API  
  - OpenAI API  

---

## 🛠 Tech Stack

- .NET MAUI
- C#
- REST APIs
- OpenAI API
- WhatsApp API
- Push Notifications
- Custom Backend API (developed separately)

---

## 🎯 Key Features

- AI-powered first-contact customer support
- Seamless handoff from AI to human attendant
- Real-time chat interface
- Push notification alerts for scheduling requests
- Designed to work around WhatsApp API limitations

---

## 🚧 Project Status

This project is currently **under active development** and evolving as new features and improvements are implemented.

---

## 📌 Notes

This application was created as a practical solution to overcome WhatsApp API constraints while maintaining a high-quality customer support experience through AI-assisted automation and human interaction.
