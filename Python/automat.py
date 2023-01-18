from typing import Dict, Optional, Set


class Automaton:
    def __init__(self) -> None:
        self.transition_function: Dict[str, Dict[str, str]] = {}
        self.start_state: Optional[str] = None
        self.end_states: Set[str] = set()
        self.alphabet: Set[str] = set()
        self.current_state: Optional[str] = None

    def accepts_word(self, word: str) -> bool:
        self.current_state = self.start_state
        for char in word:
            if char not in self.alphabet:
                return False
            if char not in self.transition_function[self.current_state]:
                self.current_state = self.start_state
            else:
                self.current_state = self.transition_function[self.current_state][char]
        return self.current_state in self.end_states


# Testy:
automaton = Automaton()
automaton.transition_function = {
    "q1": {"a": "q2"},
    "q2": {"a": "q5", "b": "q3"},
    "q3": {"b": "q4"},
    "q4": {"a": "q5"},
    "q5": {}
}
automaton.start_state = "q1"
automaton.end_states = {"q5"}
automaton.alphabet = {"a"}
print(automaton.accepts_word("a"))  # False
print(automaton.accepts_word("ahoj"))  # True
print(automaton.accepts_word("abba"))  # True
print(automaton.current_state)
print(automaton.accepts_word("abbaaa"))  # False
