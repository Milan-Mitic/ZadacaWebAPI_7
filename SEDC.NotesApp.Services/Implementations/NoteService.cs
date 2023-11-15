﻿using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Services.Interfaces;
using System.Net.WebSockets;

namespace SEDC.NotesApp.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<User> _userRepository;

        public NoteService(IRepository<Note> noteRepository, IRepository<User> userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            if (addNoteDto == null)
                throw new ArgumentNullException("Note cannot be null");

            var user = _userRepository.GetById(addNoteDto.UserId);
            if (user == null)
                throw new ArgumentException($"User with UserId {addNoteDto.UserId} not found. ");

            if (string.IsNullOrEmpty(addNoteDto.Text) || addNoteDto.Text.Length > 100)
                throw new ArgumentException("Text field must not be empty and should not exceed 100 characters");

            var note = new Note
            {
                UserId = addNoteDto.UserId, 
                Text = addNoteDto.Text,
                Priority = addNoteDto.Priority, 
                Tag = addNoteDto.Tag    
            };

            _noteRepository.Add(note);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            
            if(noteDb == null)
                throw new Exception($"Note with id {id} was not found");

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            List<Note> notes = _noteRepository.GetAll();

            List<NoteDto> noteDtos = notes.Select(NoteMapper.ToNoteDto).ToList();
            
            return noteDtos;    
        }

        public NoteDto GetById(int id)
        {
            Note note = _noteRepository.GetById(id);

            if (note == null)
                throw new Exception($"Note with id {id} not found.");

            NoteDto noteDto  = NoteMapper.ToNoteDto(note);
            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            //1.validation
            Note noteDb = _noteRepository.GetById(note.Id);
            if(noteDb == null)
                throw new Exception($"Note with id {note.Id} was not found!");
            
            User userDb = _userRepository.GetById(note.UserId);
            
            if (userDb == null)
                throw new Exception($"User with id {note.UserId} does not exist");

            if (string.IsNullOrEmpty(note.Text))
                throw new Exception("Text is required field");
            
            //Text is not null or empty
            if (note.Text.Length > 100)
                throw new Exception("Text can not contain more than 100 characters");

            //2.update
            //We must update the object that we read from db
            noteDb.Text = note.Text;
            noteDb.Priority = note.Priority;
            noteDb.Tag = note.Tag;
            noteDb.UserId = note.UserId;
            noteDb.User = userDb;

            _noteRepository.Update(noteDb);
        }
    }
}
