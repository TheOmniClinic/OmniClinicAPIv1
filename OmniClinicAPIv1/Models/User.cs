﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OmniClinicAPIv1.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Token")]
        public string Token { get; set; }
    }
}