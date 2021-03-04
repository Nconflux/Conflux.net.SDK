(function ($) {
    function validateHKID(hkid) {//refer to https://zh.wikipedia.org/wiki/%E9%A6%99%E6%B8%AF%E8%BA%AB%E4%BB%BD%E8%AD%89
        if (hkid == '') {
            return true;
        }
        hkid = hkid.replace(/\(/g, '').replace(/\)/g, '');
        var reg = /[a-zA-Z]?[a-zA-Z]\d{6}[\dAa]/;
        if (!reg.test(hkid)) {
            return false;
        }
        var arrHKID = hkid.split('').reverse();

        var sum = 0;
        for (var i = 0; i < arrHKID.length; i++) {
            if (i == 0) {//validate number
                if (arrHKID[i] == 'A' || arrHKID[i] == 'a') {
                    arrHKID[i] = 10;
                }
            } else {
                if (!$.isNumeric(arrHKID[i])) {// if it is  letter
                    arrHKID[i] = positionOfLetter(arrHKID[i]) % 11;
                }
            }
            sum += arrHKID[i] * (i + 1);
        }

        if (sum % 11 == 0) {
            return true;
        } else {
            return false;
        }
        //A, a return 1;
        function positionOfLetter(letter) {
            if (letter >= 'A' && letter <= 'Z')
                return letter.charCodeAt() - 64;
            else if (letter >= 'a' && letter <= 'z')
                return letter.charCodeAt() - 32 - 64;
        }
    }
    //static******************
    $.extend({
        validateHKID: validateHKID,
        urlParam: function (name) {
            var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            else {
                return results[1] || 0;
            }
        },
        hkid: function (options) {

            $(document).on('keyup', "input[hkid='true']", function (e) {
                $(this).click();
      
                $(this).val($(this).val().toUpperCase());
                var content = $(this).val().replace(/\(/g, '').replace(/\)/g, '').replace(/\*/g, '').toUpperCase();

                if (content.length >= 2) {
                    if (content.match(/^[A-Z][A-Z]/)) {
                        $(this).val('(' + content.substr(0, 2) + ')' + content.substr(2));
                        $(this).mask("(XX)dddddd(Z)", { placeholder: "*", autoclear: false });
                    } else {
                        $(this).val('(' + content.substr(0, 1) + ')' + content.substr(1));
                        $(this).mask("(X)dddddd(Z)", { placeholder: "*", autoclear: false });
                    }
        

                    content = $(this).val().toUpperCase();

                    var inp = $(this)[0];
                    if (inp.createTextRange) {
                        var part = inp.createTextRange();
                        part.move("character", 0);
                        part.select();
                    } else if (inp.setSelectionRange) {
                        var postion = content.indexOf('*') > -1 ? content.indexOf('*') : 18;
                        inp.setSelectionRange(postion, postion);
                    }
                    inp.focus();
                    if (content.length > 2) {
                        if (content.indexOf("*") == -1) {
                            if (!validateHKID(content)) {
                                if ($('.jqueryPluginWrongHKID:visible').length == 0) {
                                    $.smallBox({
                                        title: " Error",
                                        content: "<i class='jqueryPluginWrongHKID'>" + content + " is wrong HKID, please type correct HKID</i>",
                                        color: "#C46A69",
                                        iconSmall: "fa fa-warning shake animated",
                                        // timeout: 10000
                                    });
                                }
                            } else {
                                if ($('.jqueryPluginCorrectHKID:visible').length == 0) {
                                    $.smallBox({
                                        title: "Success",
                                        content: "<i class='jqueryPluginCorrectHKID'>" + content + " is correct HKID</i>",
                                        color: "#739E73",
                                        timeout: 10000,
                                        icon: "fa fa-check"
                                    });
                                }

                            }
                        }
                    }

                } else {
                    $(this).unmask();
                }
                var target = e.target,
                    position = target.selectionStart; // Capture initial position

                target.value = target.value.replace(/\s/g, '');  // This triggers the cursor to move.

                target.selectionEnd = 100;    // Set the cursor back to the initial position.
                $(this).click();
                $(this).focus();
                window.getSelection().removeAllRanges();
                

            });

        }
        , uppercase: function (options) {
            $(document).on('keyup', "input[uppercase='true']", function (e) {
                if ($(this).val()) {
                    $(this).val($(this).val().toUpperCase());
                }
            });
        }
    });
    //dynamic******************
    $.fn.showLinkLocation = function (options) {

        console.log($(this), 105)
        return this;
    };

    //init*******************
    $.mask.definitions['9'] = '';
    $.mask.definitions['d'] = '[0-9]';
    $.mask.definitions['Z'] = '[0-9Aa]';
    $.mask.definitions['X'] = '[A-Z]';
    $.hkid();
    $(function () {
        $.datepicker.setDefaults({
            changeYear: true,
            changeMonth: true,
            dateFormat: 'dd/mm/yy',
            prevText: '<i class="fa fa-chevron-left"></i>',
            nextText: '<i class="fa fa-chevron-right"></i>'
        });
        $.uppercase();
        $('body').on('focus', 'input[data-datepicker="true"]', function () {
            $(this).datepicker({ dateFormat: 'dd/mm/yy' }).on('change', function (ev) {
                try {
                    $(this).valid();  // triggers the validation test
                    // '$(this)' refers to '$("#datepicker")'
                } catch (e) {

                }
            });
        });
        $('document').on('keydown', 'input[data-number="true"]', function () {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        })
        setInterval(function () {
            $("input[data-hkname='true']").each(
                function (e) {
                    var self = this;
                    if (!$(self).parent().hasClass("div-hkname")) {
                        $(self).wrapAll("<div class='div-hkname' />");
                        var table = "<table> <tbody><tr><td width='45%'><input class='form-control surname'   placeholder='Surname(eg:CHAN)' uppercase='true'  style='width:90%;display:inline;'></td><td width='55%'><input class='givenname form-control'  placeholder='Given Name(eg:Siu Ming)' style='width:90%;display:inline;'></td></tr></tbody></table>";
                        $(self).after(table);
                        $(this).hide();
                        var input = $(this);
                        //Load data.
                        $(document).on('change', 'input[data-hkname="true"]', function (e) {
                            if ($.trim(input.val()) != "" && ($(self).closest('div').find('.surname').val() == "" && $(self).closest('div').find('.givenname').val() == "")) {
                                $(self).closest('div').find('.surname').val($.trim(input.val().split(',')[0]))
                                $(self).closest('div').find('.givenname').val($.trim(input.val().split(',')[1]))
                            }
                        });

                        $(document).on('keyup', '.surname,.givenname', function (e) {
                            if ($(self).closest('div').find('.givenname').val() && $(self).closest('.div-hkname').find('.surname').val()) {
                                input.val($(self).closest('.div-hkname').find('.surname').val().toUpperCase() + ', ' + $(self).closest('div').find('.givenname').val());
                            }
                        });
                        $(self).closest('div').find('.surname').val($.trim(input.val().split(',')[0]))
                        $(self).closest('div').find('.givenname').val($.trim(input.val().split(',')[1]))
                    }

                });
        }, 1000);

    });

}(jQuery));
(function ($) {
    var originalVal = $.fn.val;
    $.fn.val = function () {
        var result = originalVal.apply(this, arguments);
        if (arguments.length > 0)
            $(this).change(); // OR with custom event $(this).trigger('value-changed');
        return result;
    };
})(jQuery);