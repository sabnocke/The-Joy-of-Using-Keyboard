module chars
  implicit none

  private 
  public :: is_upper, is_question, uppercase, is_empty, response
  character(len=26),parameter :: uca = "ABCDEFGHIJKLMNOPQRSTUVWZYX"

  contains
    pure elemental logical function is_upper(ch)
      character, INTENT(IN) :: ch
      is_upper = index(uca, ch) /= 0

    end function is_upper

    pure logical function is_question(s)
      CHARACTER(*), INTENT(IN) :: s
      is_question = (index(s, "?") - len(s)) == 0
    end function is_question

    pure logical function is_empty(s)
      CHARACTER(*), INTENT(IN) :: s
      is_empty = len(s) == 0
    end function is_empty

    pure logical function uppercase(s)
      CHARACTER(*), INTENT(IN) :: s
      INTEGER :: i



      do i=1, LEN(s)
        if(.not. is_upper(s(i:i))) then
          uppercase = .false.
          return
        endif
      end do
      uppercase = .true.
    end function uppercase

   character(100) function response(s)
      CHARACTER(*), INTENT(IN) :: s
      print *, is_empty(s)
      if (uppercase(s) .and. is_question(s)) then
        response = "Calm down, I know what i'm doing!"
      else if (uppercase(s)) then
        response = "Whoa, chill out!"
      else if (is_question(s)) then
        response = "Sure."
      else if (is_empty(s)) then
        response = "Fine. Be that way!"
      else
        response = "Whatever."
      endif
    end function response 
end module chars

program main
  use chars
  character(len=5) :: Hello

  print *, "How are you? ", response("How are you?")
  print *, "HOW ARE YOU? ", response("HOW ARE YOU?")
  print *, "", response("")
  print *, "awda ",  response("awda")


end program main
