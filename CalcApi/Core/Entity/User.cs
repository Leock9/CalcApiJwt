namespace CalcApi.Core.Entity;

public record User(string Username, string Password,  IList<string> Roles);