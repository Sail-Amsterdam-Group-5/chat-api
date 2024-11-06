# SAIL Chat API Documentation

This API provides real-time chat functionality designed for the SAIL Amsterdam event, supporting volunteer communication with features like direct messages, group chats, and notifications. The service is designed to work with Azure services including Web PubSub, Cosmos DB, and Azure Notification Hub.

## 🚀 Quick Start

1. Clone the repository
2. Navigate to the project directory
3. Run the following commands:
```bash
dotnet restore
dotnet run
```
4. Open your browser and navigate to: `https://localhost:7000/swagger`

## 🧪 Testing the API with Swagger

### 1. Authentication
First, authenticate with the API:
1. Click the "Authorize" button in Swagger
2. Enter API Key: `SAIL2025`
3. Click "Authorize"

### 2. Create/View Users

View existing users:
1. Expand the `GET /api/Users/{userId}` endpoint
2. Click "Try it out"
3. Use one of these test users:
   - user-1 (Event Coordinator)
   - user-2 (Team Lead)
   - user-3 (Volunteer)
4. Click "Execute"

Create new user:
```json
{
  "notificationEnabled": true
}
```

### 3. Create/View Chats

View existing chats:
1. Expand the `GET /api/Chats/{chatId}` endpoint
2. Try with:
   - chat-1 (Group Chat)
   - chat-2 (Direct Message)

Create new chat:
```json
{
  "type": "Group",
  "participantIds": ["user-1", "user-2"],
  "adminIds": ["user-1"]
}
```

### 4. Send Messages

Send a new message:
1. Expand the `POST /api/Messages` endpoint
2. Use this sample:
```json
{
  "senderId": "user-1",
  "chatId": "chat-1",
  "content": "Hello team!",
  "type": "Text"
}
```

View chat messages:
1. Expand `GET /api/Messages/chat/{chatId}`
2. Enter chat-1 or chat-2
3. Click "Execute"

## 📝 API Endpoints Reference

### Messages

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Messages/chat/{chatId}` | Get chat messages |
| POST | `/api/Messages` | Send new message |
| DELETE | `/api/Messages/{messageId}` | Delete message (15-min window) |

### Chats

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Chats` | Create new chat |
| GET | `/api/Chats/{chatId}` | Get chat details |
| POST | `/api/Chats/{chatId}/users/{userId}` | Add user to chat |
| DELETE | `/api/Chats/{chatId}/users/{userId}` | Remove user from chat |

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Users` | Create new user |
| GET | `/api/Users/{userId}` | Get user details |
| PUT | `/api/Users/{userId}/notifications` | Update notification settings |
| GET | `/api/Users/{userId}/chats` | Get user's active chats |

## 📦 Mock Data

The API comes with pre-populated mock data:

### Users
- user-1: Event Coordinator
- user-2: Team Lead
- user-3: Volunteer

### Chats
- chat-1: Group chat with all users
- chat-2: Direct message between user-1 and user-2

### Messages
- Various test messages in both chats
- Different message statuses (Sent/Delivered/Read)

## 🔍 Testing Scenarios

1. **Group Chat Flow**:
   - Get chat-1 details
   - View participants
   - Send new message
   - View message history

2. **Direct Message Flow**:
   - Get chat-2 details
   - Send message
   - Check delivery status

3. **User Management**:
   - View user details
   - Update notification settings
   - Check active chats

4. **Message Management**:
   - Send message
   - Delete within 15 minutes
   - View chat history

## ⚠️ Notes

- Uses in-memory storage for demonstration
- API Key authentication (SAIL2024)
- 15-minute window for message deletion
- All timestamps are in UTC

## 🔄 Response Codes

- 200: Success
- 201: Created
- 204: No Content
- 400: Bad Request
- 401: Unauthorized (Invalid API Key)
- 404: Not Found

## 🏗️ Development Notes

This is a demonstration implementation. For production:
- Implement Azure services integration:
  - Cosmos DB for persistence
  - Web PubSub for real-time updates
  - Blob Storage for media
  - Notification Hub for push notifications
- Add comprehensive error handling
- Implement rate limiting
- Add monitoring and logging
- Enhance security measures
- Add input validation
- Implement real-time status updates

## 🔒 Security

- API Key Authentication (X-API-Key header)
- Rate limiting: 100 messages per hour per user
- Basic validation on all inputs
- Admin role validation for group management
