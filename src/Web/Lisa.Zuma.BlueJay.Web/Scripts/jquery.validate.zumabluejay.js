$.validator.addMethod('requiredif', function (value, element, parameters) {

    var id = '#' + parameters['dependentproperty'];

    // Get the target value (as string, that's what actual value will be)
    var targetValue = parameters['targetvalue'];
    targetValue = (targetValue == null ? '' : targetValue).toString();

    var targetValueArray = targetValue.split('|');

    for (var i = 0; i < targetValueArray.length; i++) {

        // Get actual value of the target control
        // note: this probably needs to cater for more control types, e.g. radios
        var control = $(id);
        var controlType = control.attr('type');
        var actualValue =
            controlType === 'checkbox' ?
            control.prop('checked') ? 'true' : 'false' :
            control.val();

        // If the condition is true, reuse the existing 
        // required field validator functionality
        if (targetValueArray[i] === actualValue) {

            return $.validator.methods.required.call(this, value, element, parameters);
        }
    }

    return true;
});

$.validator.unobtrusive.adapters.add('requiredif', ['dependentproperty', 'targetvalue'], function (opts) {

    opts.rules['requiredif'] = {

        dependentproperty: opts.params['dependentproperty'],
        targetvalue: opts.params['targetvalue']
    };
    opts.messages['requiredif'] = opts.message;
});