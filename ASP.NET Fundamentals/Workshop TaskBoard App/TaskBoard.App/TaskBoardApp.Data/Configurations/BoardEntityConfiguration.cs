﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Data.Configurations
{
    public class BoardEntityConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            ICollection<Board> boards = GenerateBoards();

            builder
                .HasData(boards);
        }

        private ICollection<Board> GenerateBoards()
        {
            ICollection<Board> boards = new HashSet<Board>();

            Board currentBoard;

            currentBoard = new Board()
            {
                Id = 1,
                Name = "Open",
            };

            boards.Add(currentBoard);

            currentBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };
            boards.Add(currentBoard);

            currentBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
            boards.Add(currentBoard);

            return boards;
        }
    }
}
