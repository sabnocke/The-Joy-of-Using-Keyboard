import unittest
import roman1


class KnownValues(unittest.TestCase):
    known_values = (
        (1, 'I'), (2, 'II'), (3, 'III'), (4, 'IV'), (5, 'V'), (6, 'VI'), (7, 'VII'), (8, 'VIII'), (9, 'IX'),
        (10, 'X'), (50, 'L'), (31, 'XXXI'),
        (100, 'C'), (500, 'D'),
        (1000, 'M'),
        (148, 'CXLVIII'), (294, 'CCXCIV'), (312, 'CCCXII'), (421, 'CDXXI'), (528, 'DXXVIII'), (621, 'DCXXI'),
        (782, 'DCCLXXXII'), (870, 'DCCCLXX'), (941, 'CMXLI'), (1043, 'MXLIII'),
        (1110, 'MCX'), (1226, 'MCCXXVI'), (1301, 'MCCCI'), (1485, 'MCDLXXXV'), (1509, 'MDIX'), (1607, 'MDCVII'),
        (1754, 'MDCCLIV'), (1832, 'MDCCCXXXII'), (1993, 'MCMXCIII'), (2074, 'MMLXXIV'), (2152, 'MMCLII'),
        (2212, 'MMCCXII'), (2343, 'MMCCCXLIII'), (2499, 'MMCDXCIX'), (2574, 'MMDLXXIV'), (2646, 'MMDCXLVI'),
        (2723, 'MMDCCXXIII'), (2892, 'MMDCCCXCII'), (2975, 'MMCMLXXV'), (3051, 'MMMLI'), (3185, 'MMMCLXXXV'),
        (3250, 'MMMCCL'), (3313, 'MMMCCCXIII'), (3408, 'MMMCDVIII'), (3501, 'MMMDI'), (3610, 'MMMDCX'),
        (3743, 'MMMDCCXLIII'), (3844, 'MMMDCCCXLIV'), (3888, 'MMMDCCCLXXXVIII'), (3940, 'MMMCMXL'), (3999, 'MMMCMXCIX'))

    def test_to_roman(self):
        """to_roman should give known result with known input"""
        for integer, numeral in self.known_values:
            result = roman1.to_roman(integer)
            self.assertEqual(numeral, result)

    def test_from_roman(self):
        """from_roman should give known result with known input"""
        for integer, numeral in self.known_values:
            result = roman1.from_roman(numeral)
            self.assertEqual(integer, result)


class RoundTripCheck(unittest.TestCase):
    def round_trip(self):
        """from_roman(to_roman(n)) == n for all n"""
        for integer in range(1, 4000):
            numeral = roman1.to_roman(integer)
            result = roman1.from_roman(numeral)
            self.assertEqual(numeral, result)


class ToRomanBadInput(unittest.TestCase):
    def test_too_large(self):
        """to_roman should fail with too large input"""
        self.assertRaises(roman1.OutOfRangeError, roman1.to_roman, 4000)

    def test_is_zero(self):
        """to_roman should fail with 0 input"""
        self.assertRaises(roman1.OutOfRangeError, roman1.to_roman, 0)

    def test_too_small(self):
        """to_roman should fail with 0 input"""
        self.assertRaises(roman1.OutOfRangeError, roman1.to_roman, -1)

    def test_is_float(self):
        """to_roman should fail with float input"""
        self.assertRaises(roman1.InputNotSupported, roman1.to_roman, 0.5)


class FromRomanBadInput(unittest.TestCase):
    def too_many_repeated_numerals(self):
        """from_roman should fail with too many repeated numerals"""
        for s in ["MMMM", "CCCC", "DD", "LL", "XXXX", "IIII", "VV"]:
            self.assertRaises(roman1.InvalidRomanNumeralError, roman1.from_roman, s)

    def too_many_repeated_pairs(self):
        """from_roman should fail with too many repetitions of single pair"""
        for s in ["CMCM", "CDCD", "XCXC", "XLXL", "IXIX", "IVIV"]:
            self.assertRaises(roman1.InvalidRomanNumeralError, roman1.from_roman, s)

    def test_malformed_antecedents(self):
        """from_roman should fail with malfromed antecedents"""
        for s in ["IIMXCC", "VX", "DCM", "CMM", "IXIV", "MCMC", "XCX", "IVI", "LM", "LD", "LC"]:
            self.assertRaises(roman1.InvalidRomanNumeralError, roman1.from_roman, s)


print(roman1.from_roman("MCDLXXIII"))
