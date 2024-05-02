﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EM.DOMAIN.Validations
{
	public class DataNascimentoValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{

			DateTime dataNascimento;
			if (!DateTime.TryParse(value.ToString(), out dataNascimento))
			{
				return new ValidationResult("Data de Nascimento inválida.");
			}

			if (dataNascimento >= DateTime.Now)
			{
				return new ValidationResult("Data de Nascimento deve estar no passado.");
			}

			return ValidationResult.Success;
		}
	}
}
