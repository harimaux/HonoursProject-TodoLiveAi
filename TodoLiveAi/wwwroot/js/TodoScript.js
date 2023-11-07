

$(document).ready(function () {

    $('footer').hide();

    //SHOW ADD TASK MODAL
    let addTaskModal = document.querySelector('.addTodoBtn');
    addTaskModal.addEventListener('click', async () => {

        try {

            await $.post(showAddTaskModalUri, {}, function (partialModal) {

                $('.userTaskModalBox').html(partialModal);

            });



        } catch (error) {
            console.error('An error occurred:', error);
        }

        $('.addTaskModal').modal("show");


        //ANIMATED LABELS IN 'ADD TASK' MODAL > FORM
        let customInput1 = document.querySelectorAll('.customInputLabelBox1 input, .customInputLabelBox1 textarea, .customInputLabelBox1 select');

        //MOVES LABELS IF THERE IS VALUE IN INPUT BOX

        //INITIAL VALUE CHECK
        customInput1.forEach(input => {

            if (input.value != '') {

                input.nextElementSibling.classList.add('inputHasValue');

            };
        });


        //CALENDAR WILL ALWAYS HAVE LABEL SET AT TOP (DESIGN IMPLICATIONS)
        let calendarInput = document.querySelector('.calendarInput');
        calendarInput.classList.add('inputHasValue');

        //CHECK WHILE TYPIING
        $(function () {
            $(customInput1).focus(function () {
                selectedInput = this;

            }).blur(function (e) {

                if (selectedInput.value != "") {
                    e.target.nextElementSibling.classList.add('inputHasValue');
                } else {
                    e.target.nextElementSibling.classList.remove('inputHasValue');
                    calendarInput.classList.add('inputHasValue');
                }

                selectedInput = null;

            });
        });



        //SAVE TASK FORM
        var form = document.querySelector('.createTaskForm');
        form.onsubmit = async function (e) {

            e.preventDefault();

            //INPUT VALUES
            const taskTitle = document.querySelector('input[name=Title]').value;
            const taskFromRequested = document.querySelector('input[name=FromRequested]').value;
            const taskDateDue = document.querySelector('input[name=DateDue]').value;

            //TEXTAREA VALUES
            const taskContent = document.querySelector('textarea[name=Content]').value;

            //SELECT VALUES
            const taskPriority = document.querySelector('select[name=Priority]').value;

            // LABELS ELEMENTS
            const labelTitle = document.querySelector('#labelTitle');
            const labelContent = document.querySelector('#labelContent');
            const labelFrom = document.querySelector('#labelFrom');
            const labelPriority = document.querySelector('#labelPriority');
            const labelDueDate = document.querySelector('#labelDueDate');

            //FROM VALIDATION
            let isValidated = true;

            function validateForm(inputValue, errorMessage, label) {

                if (inputValue == "" || inputValue == null) {

                    isValidated = false;

                    label.innerHTML = errorMessage;
                    label.classList.add('inputHasValue');
                    label.style.color = "red";

                } else {

                    switch (label.getAttribute('id')) {
                        case 'labelTitle':
                            getValueSetStyle()
                        case 'labelContent':
                            getValueSetStyle()
                        case 'labelFrom':
                            getValueSetStyle()
                        case 'labelPriority':
                            getValueSetStyle()
                        case 'labelDueDate':
                            getValueSetStyle()
                    }

                    function getValueSetStyle() {
                        label.innerHTML = label.getAttribute('base-value');
                        label.style.color = "#808080ff";
                    }
                };
            };

            //DEFAULT LABEL VALUES
            validateForm(taskTitle, "Enter title", labelTitle);
            validateForm(taskContent, "Enter task description", labelContent);
            validateForm(taskFromRequested, "Enter from", labelFrom);
            validateForm(taskPriority, "Set priority", labelPriority);
            validateForm(taskDateDue, "Set due date", labelDueDate);

            if (isValidated == false) {
                return false;
            }


            //SET NEW TASK
            let formModel = new FormData();

            formModel.append('Title', taskTitle);
            formModel.append('Content', taskContent);
            formModel.append('FromRequested', taskFromRequested);
            formModel.append('Priority', taskPriority);
            formModel.append('DateDue', taskDateDue);

            $.ajax({
                url: setNewTaskUri,
                type: 'POST',
                data: formModel,
                processData: false,
                contentType: false,
                success: function (data) {

                    //RESETS THE FORM AND LABELS + HIDE
                    form.reset();
                    customInput1.forEach(input => {

                        input.nextElementSibling.classList.remove('inputHasValue');

                    });
                    calendarInput.classList.add('inputHasValue');
                    $('.addTaskModal').modal("hide");

                    //SHOWS NEWLY ADDED TASK
                    $('.userTasksCard').prepend(data);
                    let addedCard = document.querySelector('.userTasksCard').firstElementChild;
                    $(addedCard).effect("shake");


                },
                error: function (xhr, status, error) {
                    var errorMessage = xhr.responseText || 'An error occurred.';
                    alert('Error: ' + errorMessage);
                }
            });
        };

    });






    //DELETE, EDIT, COMPLETE TASK
    const cardBtns = document.querySelector('.userTasksCard');

    cardBtns.addEventListener('click', function (e) {

        if (e.target.matches('.cardFooterShowConfirmBoxBtn')) {

            //Hides all confirm boxes
            let cardFooterConfirmBox = document.querySelectorAll('.cardFooterConfirmBox');
            cardFooterConfirmBox.forEach(item => {

                item.classList.remove('cardFooterConfirmBoxShow');
            });

            //removes stle from all Li elements - background color
            let cardFooterBtsWithConfirm = document.querySelectorAll('.cardFooterBtsWithConfirm');
            cardFooterBtsWithConfirm.forEach(item => {

                item.style.backgroundColor = "unset";
            });

            //Shows confirm box
            let confirmBox = e.target.previousElementSibling;
            confirmBox.classList.add('cardFooterConfirmBoxShow');

            let cardFooterLiElement = e.target;
            cardFooterLiElement.parentNode.style.backgroundColor = "#B1B0B1";

            //removes style of all buttons + add back hover
            let cardFooterShowConfirmBoxBtn = document.querySelectorAll('.cardFooterShowConfirmBoxBtn');
            cardFooterShowConfirmBoxBtn.forEach(item => {

                item.classList.add('cardFooterButtonsHover');
                item.style.color = "#F2F2F2";
            });

            //changes style of called button
            e.target.style.color = "#050505";
            e.target.style.opacity = ".7";
            e.target.classList.remove('cardFooterButtonsHover');

            //Close confirm box btn
            let allCloseBtns = document.querySelectorAll('.cardFooterCancelConfirmBox');
            allCloseBtns.forEach(item => {

                item.addEventListener('click', (ev) => {

                    item.parentElement.parentElement.nextElementSibling.classList.add('cardFooterButtonsHover')
                    item.parentElement.parentElement.classList.remove('cardFooterConfirmBoxShow');
                    item.parentElement.parentElement.parentElement.style.backgroundColor = "unset";
                    e.target.style.color = "#F2F2F2";
                    e.target.style.opacity = "1";
                });
            });

            //DELETE TASK
            let cardId = e.target.getAttribute('cardId');
            let cardBox = e.target.closest('.customCard');

            let removeBtn = cardBox.getElementsByClassName('cardFooterDeleteTaskBtn')[0];

            removeBtn.addEventListener('click', async () => {
                try {
                    await $.post(deleteTaskUri, { cardId: cardId });
                    $(cardBox).hide("puff", function () {
                        $(cardBox).remove();
                    });
                } catch (error) {
                    console.error('An error occurred:', error);
                }
            });


            // Mark task as complete
            let completeTaskBtn = cardBox.getElementsByClassName('cardFooterMarkTaskAsCompleteBtn')[0];

            completeTaskBtn.addEventListener('click', async () => {
                try {
                    await $.post(markCompleteTaskUri, { cardId: cardId });
                    $(cardBox).hide("puff", function () {
                        $(cardBox).remove();
                    });
                } catch (error) {
                    console.error('An error occurred:', error);
                }
            });



        };
    });



    //ADD UNDERLINE TO A LINK







    const userTasksLeftMenu = document.querySelector('.userTasksLeftMenu');
    userTasksLeftMenu.addEventListener('click', function (e) {

        // TASK SORTING - LEFT MENU
        if (e.target.matches('.menuLeftShowCompleted, .menuLeftShowLow, .menuLeftShowOverdue, .menuLeftShowUrgent, .menuLeftShowImportant, .menuLeftShowMedium, .menuLeftShowLow')) {
            let selectedTasksName = e.target.innerText;

            $.post(showSelectedTasksUri, { id: selectedTasksName })

                .done(function (data) {
                    $('.userTasksCard').html(data);
                })

                .fail(function (error) {
                    console.error('An error occurred:', error);
                });

        };



        //USER TASKS - LEFT MENU
        if (e.target.matches('.menuLeftShowPriority, .priorityColseBtn')) {

            const priorityBtnsBox = document.querySelector('.priorityBtnsBox');
            priorityBtnsBox.classList.toggle('priorityBtnsBoxShow');

        }

    });




    //AI TASK SUPPORT
    const cardsContainer = document.querySelector('.userTasksCard');

    cardsContainer.addEventListener('click', (e) => {

        if (e.target.matches('.cardFooterLeftBtnsAiHelp')) {

            let cardParent = $(e.target).parents()[3];
            let cardContent = $(cardParent).find('.customCardContent > p')[0];
            let loadingIcon = $(cardParent).find('.cardFooterLeftLoader')[0];
            let message = cardContent.innerHTML;
            let cardId = e.target.getAttribute('cardId');

            loadingIcon.style.display = "inline-block";

            try {

                $.post(getAiUri, { cardId: cardId, message: message }, function (data) {

                    $(".aiResponseModalBox").remove();
                    $(".aiResponseBox").html(data);
                    $('.aiResponseModalBox').modal("show");
                    loadingIcon.style.display = "none";

                });

            } catch (error) {

                console.error(error);
                loadingIcon.style.display = "none";
                alert(error);

            }



        }

    });


});