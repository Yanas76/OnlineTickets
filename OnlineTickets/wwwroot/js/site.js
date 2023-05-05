let users = [];


/*function addItem() {

    let nameInput = document.getElementById("add-name");
    let emailAddressInput = document.getElementById("add-emailAddress");
    let phoneNumberInput = document.getElementById("add-phoneNumber");
    let dateOfBirthInput = document.getElementById("add-dateOfBirth");

    let newUser = {
        name: nameInput.value,
        emailAddress: emailAddressInput.value,
        phoneNumber: phoneNumberInput.value,
        dateOfBirth: dateOfBirthInput.value
    };

    nameInput.value = "";
    emailAddressInput.value = "";
    phoneNumberInput.value = "";
    dateOfBirthInput.value = "";

    fetch('/api/users', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newUser)
    })
        
    getUsers();
}*/

function addItem() {
    // Get the input values
    let nameInput = document.getElementById("add-name");
    let emailAddressInput = document.getElementById("add-emailAddress");
    let phoneNumberInput = document.getElementById("add-phoneNumber");
    let dateOfBirthInput = document.getElementById("add-dateOfBirth");

    // Create a new user object
    let newUser = {
        name: nameInput.value,
        emailAddress: emailAddressInput.value,
        phoneNumber: phoneNumberInput.value,
        dateOfBirth: dateOfBirthInput.value
    };

    // Reset the input values
    nameInput.value = "";
    emailAddressInput.value = "";
    phoneNumberInput.value = "";
    dateOfBirthInput.value = "";

    fetch('/api/users', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newUser)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            getUsers();
        })
        .catch(error => console.error('Unable to add user', error));
}

// Function to update a user
function updateItem() {
    // Get the input values
    let idInput = document.getElementById("edit-id");
    let isCompleteInput = document.getElementById("edit-isComplete");
    let nameInput = document.getElementById("edit-name");
    let emailAddressInput = document.getElementById("edit-emailAddress");
    let phoneNumberInput = document.getElementById("edit-phoneNumber");
    let dateOfBirthInput = document.getElementById("edit-dateOfBirth");

    // Get the user to update
    let user = users.find(u => u.id == idInput.value);

    // Update the user properties
    user.isComplete = isCompleteInput.checked;
    user.name = nameInput.value;
    user.emailAddress = emailAddressInput.value;
    user.phoneNumber = phoneNumberInput.value;
    user.dateOfBirth = dateOfBirthInput.value;

    // Clear the input fields and hide the edit form
    idInput.value = "";
    isCompleteInput.checked = false;
    nameInput.value = "";
    emailAddressInput.value = "";
    phoneNumberInput.value = "";
    dateOfBirthInput.value = "";
    document.getElementById("editForm").style.display = "none";

    // Update the table
    updateTable();
}

// Function to delete a user
function deleteItem(id) {
    // Find the index of the user to delete
    let index = users.findIndex(u => u.id == id);

    // Remove the user from the list
    users.splice(index, 1);

    // Update the table and the counter
    updateTable();
    updateCounter();
}

// Function to open the edit form
function openEditForm(id) {
    // Get the user to edit
    let user = users.find(u => u.id == id);

    // Fill the edit form with the user data
    document.getElementById("edit-id").value = user.id;
    document.getElementById("edit-isComplete").checked = user.isComplete;
    document.getElementById("edit-name").value = user.name;
    document.getElementById("edit-emailAddress").value = user.emailAddress;
    document.getElementById("edit-phoneNumber").value = user.phoneNumber;
    document.getElementById("edit-dateOfBirth").value = user.dateOfBirth;

    // Show the edit form
    document.getElementById("editForm").style.display = "block";
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function getUsers() {
    fetch('/api/users')
        .then(response => response.json())
        .then(data => {
            let rows = '';
            data.forEach(user => {
                rows += `<tr>
                   
                    <td>${user.name}</td>
                    <td>${user.emailAddress}</td>
                    <td>${user.phoneNumber}</td>
                    <td>${user.dateOfBirth}</td>
                    <td><a onclick="editItem(${user.id})">Edit</a></td>
                    <td><a onclick="deleteItem(${user.id})">Delete</a></td>
                 </tr>`;
            });
            document.querySelector('table tbody').innerHTML = rows;
            document.getElementById('counter').innerHTML = `Total Users: ${data.length}`;
        });
}


/*function editItem(id) {
    // make a GET request to the API to get the user data
    fetch(`/api/users/${id}`)
        .then(response => response.json())
        .then(data => {
            // set the input values to the user data
            document.getElementById('edit-id').value = data.id;
            document.getElementById('edit-isComplete').checked = data.isComplete;
            document.getElementById('edit-name').value = data.name;
            document.getElementById('edit-emailAddress').value = data.emailAddress;
            document.getElementById('edit-phoneNumber').value = data.phoneNumber;
            document.getElementById('edit-dateOfBirth').value = data.dateOfBirth;
            // show the edit form
            document.getElementById('editForm').style.display = 'block';
        });
}*/

function editItem(id) {
    // make a GET request to the API to get the user data
    fetch(`/api/users/${id}`)
        .then(response => response.json())
        .then(data => {
            // set the input values to the user data
            document.getElementById('edit-id').value = data.id;
            document.getElementById('edit-isComplete').checked = data.isComplete;
            document.getElementById('edit-name').value = data.name;
            document.getElementById('edit-emailAddress').value = data.emailAddress;
            document.getElementById('edit-phoneNumber').value = data.phoneNumber;
            document.getElementById('edit-dateOfBirth').value = data.dateOfBirth;
            // show the edit form
            document.getElementById('editForm').style.display = 'block';

            // handle form submission
            document.querySelector('#editForm form').onsubmit = async function (event) {
                event.preventDefault();

                // build user update model from form data
                const userUpdateModel = {
                    id: data.id,
                    isComplete: document.getElementById('edit-isComplete').checked,
                    name: document.getElementById('edit-name').value,
                    emailAddress: document.getElementById('edit-emailAddress').value,
                    phoneNumber: document.getElementById('edit-phoneNumber').value,
                    dateOfBirth: document.getElementById('edit-dateOfBirth').value,
                };

                // make PUT request to API to update user
                const response = await fetch(`/api/users/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(userUpdateModel)
                });

                // check for success status code
                if (response.ok) {
                    // refresh user list
                    getUsers();
                    // hide the edit form
                    closeInput();
                }
            };
        });
}