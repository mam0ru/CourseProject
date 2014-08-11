﻿(function() {
  $(function() {
    $(document).ready($('#addTag').on('click', function(e) {
      var child, parent;
      e.preventDefault();
      e.stopPropagation();
      parent = document.createElement('row');
      parent.className = 'row';
      child = document.createElement('input');
      child.type = 'text';
      child.setAttribute('data-autocomplete-source', "/Exercise/TagAutocompliteSearch");
      parent.appendChild(child);
      $('#Tags').append(parent);
      return $("[data-autocomplete-source]").each(function() {
        var target;
        target = $(this);
        return target.autocomplete({
          source: target.attr("data-autocomplete-source")
        });
      });
    }));
    return $("[data-autocomplete-source]").each(function() {
      var target;
      target = $(this);
      return target.autocomplete({
        source: target.attr("data-autocomplete-source")
      });
    });
  });

}).call(this);

//# sourceMappingURL=aoutocomplite.js.map
