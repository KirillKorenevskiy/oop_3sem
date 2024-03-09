using System;

public partial class Question
{
    public override string ToString()
    {
        return $"Вопрос: {Text}\nВарианты ответа: {string.Join(", ", AnswerOptions)}";
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash += Text.GetHashCode();
        hash += AnswerOptions.GetHashCode();
        hash += CorrectAnswer.GetHashCode();
        return hash;
    }
}
