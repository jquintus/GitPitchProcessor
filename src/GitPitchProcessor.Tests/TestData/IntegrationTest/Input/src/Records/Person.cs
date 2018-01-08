public sealed class Person : IEquatable<Person>, IStructuralEquatable, IComparable<Person>, IComparable, IStructuralComparable
{
	internal string _firstName;
	internal string _lastName;
	public string FirstName => _firstName;
	public string LastName => _lastName;

	public Person(string firstName, string lastName)
	{
		_firstName = FirstName;
		_lastName = LastName;
	}

	public override string ToString()
	{
		var format = new PrintfFormat<FSharpFunc<Person, string>, Unit, string, string, Person>("%+A");
		return ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Person, string>>(format).Invoke(this);
	}

	public sealed override int CompareTo(Person obj)
	{
		if (this != null)
		{
			if (obj == null) return 1;
			IComparer genericComparer = LanguagePrimitives.GenericComparer;
			int num = string.CompareOrdinal(_firstName, obj._firstName);
			if (num < 0) return num;
			if (num > 0) return num;
			IComparer genericComparer2 = LanguagePrimitives.GenericComparer;
			return string.CompareOrdinal(_lastName, obj._lastName);
		}
		else
		{
			if (obj != null)
			{
				return -1;
			}
			return 0;
		}
	}

	public sealed override int CompareTo(object obj)
	{
		return this.CompareTo((Person)obj);
	}

	public sealed override int CompareTo(object obj, IComparer comp)
	{
		return CompareTo(obj as Person);
	}

	public sealed override int GetHashCode(IEqualityComparer comp)
	{
		if (this != null)
		{
			int num = 0;
			int arg_35_0 = -1640531527;
			string text = _lastName;
			num = arg_35_0 + (((text == null) ? 0 : text.GetHashCode()) + ((num << 6) + (num >> 2)));
			int arg_65_0 = -1640531527;
			string text2 = _firstName;
			return arg_65_0 + (((text2 == null) ? 0 : text2.GetHashCode()) + ((num << 6) + (num >> 2)));
		}
		return 0;
	}

	public sealed override int GetHashCode()
	{
		return this.GetHashCode(LanguagePrimitives.GenericEqualityComparer);
	}

	public sealed override bool Equals(object obj, IEqualityComparer comp)
	{
		if (this == null) return obj == null;
		Person person = obj as Person;
		if (person != null)
		{
			return string.Equals(_firstName, person._firstName) && string.Equals(_lastName, person._lastName);
		}
		return false;
	}

	public sealed override bool Equals(Person obj)
	{
		if (this != null)
		{
			return obj != null && string.Equals(_firstName, obj._firstName) && string.Equals(_lastName, obj._lastName);
		}
		return obj == null;
	}

	public sealed override bool Equals(object obj)
	{
		Person person = obj as Person;
		return person != null && this.Equals(person);
	}
}