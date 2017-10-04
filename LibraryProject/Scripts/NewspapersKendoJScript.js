﻿function NewspaperAction(newspaperList, newspapersPublishers, userRole) //userRole ==>> Bool
{
    var Allow = function () {
        if (userRole) {
            return false;
        }
        if (!userRole) {
            return true;
        }
    }
    var popupwindow;
    var detailsTemplate;

    $(document).ready(function () {
        var flag = Allow();

        $('#grid-newspaper').kendoGrid({
            dataSource: {
                transport: {
                    read: function (options) {
                        options.success(newspaperList);
                    },

                    destroy: function (options) {
                        $.ajax({
                            url: "/Home/ConfirmedDeleteNewspaper/" + options.data.Id,
                            dataType: 'json',
                            type: 'POST',
                        });
                    },

                    update: function (options) {
                        $.ajax({
                            type: 'POST',
                            url: "/Home/EditNewspaper/" + options.data.Id,
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(options.data),
                            dataType: 'json',
                            success: function (data) {
                                options.success(data);
                            }
                        });
                    },

                    create: function (options) {
                        $.ajax({
                            type: 'POST',
                            url: "/Home/CreateNewNewspaper",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(options.data),
                            dataType: 'json',
                            success: function (data) {
                                options.success(data);
                            }
                        });
                    },

                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    },
                },
                schema: {
                    data: function (data) {
                        return data;
                    },
                    total: function (data) {
                        return data['odata.count'];
                    },
                    model: {
                        id: "Id",
                        fields: {
                            Id: { type: "number" },
                            Name: {
                                type: "string",
                                validation:
                                {
                                    required: true,
                                    namevalidation: function (input) {
                                        if (input.is("[name='Name']") && input.val() !== "") {
                                            input.attr("data-namevalidation-msg", "Name should start with capital letter");
                                            return /^[A-Z]/.test(input.val());
                                        }
                                        return true;
                                    }
                                }
                            },
                            Category: {
                                type: "string",
                                validation:
                                {
                                    required: true,
                                    authorvalidation: function (input) {
                                        if (input.is("[name='Category']") && input.val() !== "") {
                                            input.attr("data-authorvalidation-msg", "Category name should start with capital letter");
                                            return /^[A-Z]/.test(input.val());
                                        }
                                        return true;
                                    }
                                }
                            },
                            Publisher: {
                                type: "string",
                                validation:
                                {
                                    required: true,
                                    publishervalidation: function (input) {
                                        if (input.is("[name='Publisher']") && input.val() !== "") {
                                            input.attr("data-publishervalidation-msg", "Publisher name should start with capital letter");
                                            return /^[A-Z]/.test(input.val());
                                        }
                                        return true;
                                    }
                                }
                            },
                            Price: { type: "number", validation: { min: 0, required: true } }
                        },
                    }
                },
                pagesize: 10,

            },
            pageable: {
                pageSizes: true,
                buttonCount: 5,
            },
            filterable: {
                extra: false,
                operators: {
                    string: {
                        startswith: "Starts with",
                        eq: "Is equal to",
                        neq: "Is not equal to"
                    }
                }
            },
            toolbar: ["create"],

            columns: [
                { field: "Id", title: "ID", filterable: false, hidden: true },
                { field: "Name", title: "Name", filterable: false },
                { field: "Category", title: "Category", filterable: false },
                { field: "Publisher", title: "Publisher", filterable: { ui: publisherFilter } },
                { field: "Price", title: "Price", filterable: false },
                { command: ["edit", "destroy"], title: "Action", hidden: flag },                 //HIDE element
                { command: { text: "View Details", click: showDetails }, title: " ", width: "180px" }
            ],
            editable: "inline",
        });

        popupwindow = $("#details")
            .kendoWindow({
                title: "Newspaper Details",
                modal: true,
                visible: false,
                resizable: false,
                width: 300
            }).data("kendoWindow");

        detailsTemplate = kendo.template($("#template").html());
        hideAddButton(flag);
    });

    var hideAddButton = function HideToolbarWithRole(flag) {
        if (flag) {
            $(".k-grid-add", "#grid-newspaper").hide();
            $(".k-grid-toolbar").hide();
        }
    }

    function publisherFilter(element) {
        element.kendoAutoComplete({
            dataSource: newspapersPublishers,
        });
    }

    function showDetails(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        popupwindow.content(detailsTemplate(dataItem));
        popupwindow.center().open();
    }
}