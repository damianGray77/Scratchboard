static string[] ones = new[] { "", "one ", "two ", "three ", "four ", "five ", "six ", "seven ", "eight ", "nine ", "ten ", "eleven ", "twelve ", "thirteen ", "fourteen ", "fifteen ", "sixteen ", "seventeen ", "eighteen ", "nineteen " };
static string[] tens = new[] { "", "", "twenty ", "thirty ", "fourty ", "fifty ", "sixty ", "seventy ", "eighty ", "ninety " };
static string[] bigs = new[] { "", "thousand ", "million ", "billion ", "trillion ", "quadrillion ", "quintillion ", "sextillion ", "septillion ", "octillion ", "nontillion ", "dectillion " };

// supports numbers up to max 64bit int (9,223,372,036,854,775,807)
// for numbers bigger than that I would have to redesign algorithm
// to work with strings rather than numbers
string number_to_word(long n) {
  if(0 == n) { return "zero"; }

  bool neg = n < 0;
  n = Math.Abs(n);

  string ret = "";
  int j = 0;
  for(int i = get_number_length(n); i > 0; i -= 3) {
    int seg = get_digits(n, i - 2, i);

    string segstr = get_segment(seg);
    if (0 == j && 0 != seg && seg < 100 && n != seg) { segstr = "and " + segstr; }
    if ("" != segstr) { segstr += bigs[j]; }

    ret = segstr + ret;

    ++j;
    i -= 3;
  }

  return (!neg ? "" : "negative ") + ret.Substring(0, ret.Length - 1);
}

string get_segment(int seg) {
  string ret = "";

  if (seg >= 100) {
    ret = ones[get_digits(seg, 1, 1)] + "hundred ";
    seg %= 100;
  }

  if ("" != ret && 0 != seg) { ret += "and "; }

  if (seg >= 20) {
    ret += tens[get_digits(seg, 1, 1)];
    ret += ones[get_digits(seg, 2, 2)];
  } else {
    ret += ones[seg];
  }

  return ret;
}

int get_number_length(long n) {
  return 0 == n ? 1 : (int)Math.Log10(n) + 1;
}

int get_digits(long n, int from, int to) {
  int len = get_number_length(n);

  long low = (long)Math.Pow(10, len - to);
  long high = (long)Math.Pow(10, len - from + 1);

  return 0 == low ? 0 : (int)((n % high - n % low) / low);
}
