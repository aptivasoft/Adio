function WidgetPlces() {
    var _WidgetPlces = document.createElement('div');
    $(_WidgetPlces).addClass('widget-place');
    return _WidgetPlces;
}

function WidgetContainer(id, movable, collapsable, removable, editable, closeconfirm, reSizable, titleText) {

    this._id = id;
    this._movable = movable;
    this._collapsable = collapsable;
    this._removable = removable;
    this._editable = editable;
    this._closeconfirm = closeconfirm;
    this._reSizable = reSizable;
    this._WidgetContainerM = null;

    this.WidgetContainerDiv = null;
    this.widgetCallBack = { callbacks: {

        // When a Widget is added on demand, send the widget object and place ID
        onAdd: null,

        // When a editbox is closed, send the link and the widget objects
        onEdit: null,

        // When a Widget is show, send the widget object
        onShow: null
    }
    };

    var self = this;
    self.initializeWidget = function Creat() {
        var _WidgetContainer = document.createElement('div');
        $(_WidgetContainer).attr('id', id);
        $(_WidgetContainer).addClass('widget');
        $(_WidgetContainer).addClass('WidgetContainerDIV');

        if (self._movable) {
            $(_WidgetContainer).addClass('movable');
        }
        if (self._collapsable) {
            $(_WidgetContainer).addClass('collapsable');
        }
        if (self._removable) {
            $(_WidgetContainer).addClass('removable');
        }
        if (self._editable) {
            $(_WidgetContainer).addClass('editable');
        }
        if (self._closeconfirm) {
            $(_WidgetContainer).addClass('closeconfirm');
        }
        if (self._reSizable) {
            $(_WidgetContainer).addClass('cloudFXResizableWidget');
        }

        $(_WidgetContainer).addClass('ui-widget-content');

        var _Widgetheader = document.createElement('div');
        $(_Widgetheader).addClass('widget-header');
        $(_Widgetheader).addClass('ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix');
        var _Tittle = document.createElement('label');
        $(_Tittle).html(titleText);
        _Widgetheader.appendChild(_Tittle);

        var _Widgetcontent = document.createElement('div');
        $(_Widgetcontent).addClass('WidgetcontentDIV');
        $(_Widgetcontent).addClass('widget-content');
        this.WidgetContainerDiv = _Widgetcontent;

        _WidgetContainer.appendChild(_Widgetheader);
        _WidgetContainer.appendChild(_Widgetcontent);

        this.widgetCallBack.callbacks.onShow();
        self._WidgetContainerM = _WidgetContainer;
        return _WidgetContainer;
    };
}