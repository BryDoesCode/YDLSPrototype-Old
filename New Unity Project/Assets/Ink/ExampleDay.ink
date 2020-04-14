INCLUDE RetailWork.ink
INCLUDE Apartment.ink


//External Functions for Unity

EXTERNAL EndGame()

/*--------------------------------------------------------------------------------

	Variables
	
--------------------------------------------------------------------------------*/


// Stat Variables

VAR energy = 8
VAR health = 6
VAR wellness = 7


// Date Variables 

VAR month = "May"
VAR date = 11
VAR fullDate = ""


// Day Variables

LIST day = Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
VAR yesterday = Tuesday
VAR today = Wednesday
VAR tomorrow = Thursday


// Time Variables

LIST timeSlots = Morning, Midday, Evening
VAR time = Morning


// Location Variables

VAR location = ""
VAR background = ""


// Initialize Unity UI

~ energy = 8
~ health = 6
~ wellness = 7
~ fullDate = month + " " + date
~ today = Sunday
~ time = Evening
~ location = "Apartment"
~ background = "apartmentMorning"




/*--------------------------------------------------------------------------------

	Start the demo!

--------------------------------------------------------------------------------*/

-> morning








/*--------------------------------------------------------------------------------

    Stat Update Functions

--------------------------------------------------------------------------------*/


=== function ResetStats
~ energy = 8
~ health = 6
~ wellness = 7
~ month = "May"
~ date = 11
~ fullDate = month + " " + date
~ today = Wednesday
~ time = Evening
~ location = "Apartment"
~ background = "apartmentMorning"


/*--------------------------------------------------------------------------------

    Time Passage Functions

--------------------------------------------------------------------------------*/

=== function AdvanceWeekday
{ 
    - today == Sunday:
        ~ yesterday ++
        ~ today = Monday
        ~ tomorrow ++
    - today == Saturday:
        ~ yesterday ++
        ~ today ++
        ~ tomorrow = Monday
    - today == Monday:
        ~ yesterday = Monday
        ~ today ++
        ~ tomorrow ++
    - else:
        ~ yesterday ++
        ~ today ++
        ~ tomorrow ++
}


/*--------------------------------------------------------------------------------

	Fallback External Functions

--------------------------------------------------------------------------------*/


=== function EndGame()
~ return
