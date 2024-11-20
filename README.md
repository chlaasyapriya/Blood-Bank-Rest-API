# Blood Bank Rest API

The Model 'BloodBankEntry' has the following attributes-
  Id
  DonorName
  Age
  BloodType
  ContactInfo
  Quantity
  CollectionDate
  ExpirationDate
  Status


While adding and updating the entries, the following are taken into consideration-
 - the age of the donor must be greater than 18 years
 - the quantity of blood donated must not be equal to 0

For every endpoint, bad requets are taken into consideration and are handled.



•	GET:   https://localhost:7177/api/BloodBank  -- Retrieves all the entries present

•	GET:   https://localhost:7177/api/BloodBank/2  -- Retrieves the entry with id=2

•	POST:   https://localhost:7177/api/BloodBank  -- Adds new entry

•	PUT:   https://localhost:7177/api/BloodBank/5 -- Updates the entry with id=5

•	DELETE:  https://localhost:7177/api/BloodBank/5  -- Deletes the entry with id=5

•	GET:   https://localhost:7177/api/BloodBank/page=2&size=2  -- Displays the entries in page number 2, with each page having a size of 2

•	GET:  https://localhost:7177/api/BloodBank/searchbloodType=A%2B -- Retrieves all the entries that have the blood type as A+

•	GET:  https://localhost:7177/api/BloodBank/searchstatus=Requested -- Retrieves all the entries that have the status as Requested

•	GET:  https://localhost:7177/api/BloodBank/searchdonorName=Jessica  -- Retrieves all the entries that have donorname as Jessica

•	GET:  https://localhost:7177/api/BloodBank/search?parameter=bloodtype&key=AB%2B  -- Retrieves all the entries that have blood type as AB+

•	GET:  https://localhost:7177/api/BloodBank/filter?bloodType=A%2B&status=Available -- Retrieves all the entries that have A+ as blood type and status is Available

•	GET:  https://localhost:7177/api/BloodBank/sort?parameter=Collectiondate&order=desc  -- Retrieves all the entries in the descending order of their collection date

