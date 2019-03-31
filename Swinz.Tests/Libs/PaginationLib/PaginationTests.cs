﻿//-----------------------------------------------------------------------
// <copyright file="PaginationTests.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

using PaginationLib;

namespace Swinz.Tests.Libs.PaginationLib
{
    using Xunit;

    /// <summary>
    /// Unit tests for pagination library
    /// </summary>
    public class PaginationTests
    {
        [Fact]
        public void GetPage_IdentifierEqualToOne_ReturnsZeroToFive()
        {
            // Arrange
            var pagination = new PaginationTransferObject {PageIdentifier = 1};

            // Act
            var result = Paginator.GetPage(pagination);
            var expected = new PaginatorResult {From = 0, To = 5};

            // Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void GetPage_IdentifierNotEqualToOne_ReturnsFiveToTen()
        {
            // Arrange
            var pagination = new PaginationTransferObject { PageIdentifier = 2 };

            // Act
            var result = Paginator.GetPage(pagination);
            var expected = new PaginatorResult { From = 5, To = 10 };

            // Assert
            Assert.Equal(result, expected);
        }


        [Fact]
        public void GetPage_IdentifierNotEqualToOne_ReturnsTenToFifteen()
        {
            // Arrange
            var pagination = new PaginationTransferObject { PageIdentifier = 3 };

            // Act
            var result = Paginator.GetPage(pagination);
            var expected = new PaginatorResult { From = 10, To = 15 };

            // Assert
            Assert.Equal(result, expected);
        }
    }
}
