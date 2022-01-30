let toPigLatin (word: string) =
	let isVowel (c: char) = 
		match c with
		| 'a' | 'e' | 'i' | 'o' | 'u'
		| 'A' | 'E' | 'I' | 'O' | 'U' -> true
		| _ -> false
	match isVowel word.[0] with
		| true -> word + "yay"
		| _ -> word.[1..] + string(word.[0]) + "ay"