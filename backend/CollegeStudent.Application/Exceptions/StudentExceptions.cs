namespace CollegeStudent.Application.Exceptions;

public class StudentNotFoundException : Exception
{
    public StudentNotFoundException(string id)
        : base($"Student with ID {id} not found.") { }
    
}

public class StudentAlreadyExistsException : Exception
{
    public StudentAlreadyExistsException(string id)
        : base($"Student with ID {id} already exists.") { }
}