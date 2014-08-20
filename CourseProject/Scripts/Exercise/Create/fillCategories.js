(function() {
  $(document).ready(function() {
    var data;
    return data = $.getJSON("/Exercise/GetCategoties", function(data, textStatus, jqXHR) {
      return $.each(data, function(key, val) {
        var item;
        item = document.createElement('option');
        item.text = val;
        return $('#Category').append(item);
      });
    });
  });

}).call(this);

//# sourceMappingURL=fillCategories.js.map
